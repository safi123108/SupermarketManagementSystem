<?php
session_start();

include __DIR__ . '/../../../templates/auth_check.php';
include __DIR__ . '/../../../templates/header.php';
include __DIR__ . '/../../../templates/navbar.php';
include __DIR__ . '/../../../templates/sidebar.php';
require_once __DIR__ . '/../../../config/database.php';

if (!isset($_GET['id'])) {
    header("Location: ?page=manage_orders");
    exit;
}

$order_id = intval($_GET['id']);

$user_id = $_SESSION['user_id'] ?? null;

// =========================
// FETCH ORDER
// =========================
$order_stmt = $conn->prepare("
    SELECT 
        o.*,
        c.name,
        c.phone,
        cr.courier_name
    FROM orders o
    LEFT JOIN customers c ON o.customer_id = c.id
    LEFT JOIN couriers cr ON o.courier_id = cr.id
    WHERE o.id = ?
");

$order_stmt->bind_param("i", $order_id);
$order_stmt->execute();

$order = $order_stmt->get_result()->fetch_assoc();

if (!$order) {
    header("Location: ?page=manage_orders");
    exit;
}

// =========================
// SAVE FOLLOWUP
// =========================
if ($_SERVER['REQUEST_METHOD'] == 'POST') {

    $status  = $_POST['followup_status'];
    $remarks = trim($_POST['remarks']);

    $conn->begin_transaction();

    try {

        // SAVE LOG
        $log_stmt = $conn->prepare("
            INSERT INTO order_followups
            (order_id, followup_status, remarks, created_by)
            VALUES (?, ?, ?, ?)
        ");

        $log_stmt->bind_param(
            "issi",
            $order_id,
            $status,
            $remarks,
            $user_id
        );

        $log_stmt->execute();

        // =========================
        // DELIVERED
        // =========================
        if ($status == 'Delivered') {

            $update = $conn->prepare("
                UPDATE orders
                SET order_status='Delivered',
                    payment_status='Paid'
                WHERE id=?
            ");

            $update->bind_param("i", $order_id);
            $update->execute();
        }

        // =========================
        // RETURNED
        // =========================
        if ($status == 'Returned') {

            // Restore stock
            $items_stmt = $conn->prepare("
                SELECT *
                FROM order_items
                WHERE order_id=?
            ");

            $items_stmt->bind_param("i", $order_id);
            $items_stmt->execute();

            $items = $items_stmt->get_result();

            while ($item = $items->fetch_assoc()) {

                // PRODUCT
                if ($item['item_type'] == 'product') {

                    $restore = $conn->prepare("
                        UPDATE products
                        SET stock_quantity = stock_quantity + ?
                        WHERE id=?
                    ");

                    $restore->bind_param(
                        "ii",
                        $item['quantity'],
                        $item['product_id']
                    );

                    $restore->execute();
                }

                // DEAL
                if ($item['item_type'] == 'deal') {

                    $deal_stmt = $conn->prepare("
                        SELECT product_id, quantity
                        FROM deal_items
                        WHERE deal_id=?
                    ");

                    $deal_stmt->bind_param("i", $item['deal_id']);
                    $deal_stmt->execute();

                    $deal_products = $deal_stmt->get_result();

                    while ($deal_product = $deal_products->fetch_assoc()) {

                        $restore_qty = $deal_product['quantity'] * $item['quantity'];

                        $restore_deal = $conn->prepare("
                            UPDATE products
                            SET stock_quantity = stock_quantity + ?
                            WHERE id=?
                        ");

                        $restore_deal->bind_param(
                            "ii",
                            $restore_qty,
                            $deal_product['product_id']
                        );

                        $restore_deal->execute();
                    }
                }
            }

            $update = $conn->prepare("
                UPDATE orders
                SET order_status='Returned'
                WHERE id=?
            ");

            $update->bind_param("i", $order_id);
            $update->execute();
        }

        // =========================
        // IN TRANSIT
        // =========================
        if ($status == 'In Transit') {

            $update = $conn->prepare("
                UPDATE orders
                SET order_status='Dispatched'
                WHERE id=?
            ");

            $update->bind_param("i", $order_id);
            $update->execute();
        }

        $conn->commit();

        $_SESSION['success'] = "Follow-up updated successfully.";

        header("Location: ?page=followup_order&id=" . $order_id);
        exit;

    } catch (Exception $e) {

        $conn->rollback();

        $error = "Follow-up update failed.";
    }
}

// =========================
// FETCH LOGS
// =========================
$logs_stmt = $conn->prepare("
    SELECT 
        f.*,
        u.username
    FROM order_followups f
    LEFT JOIN users u ON f.created_by = u.id
    WHERE f.order_id = ?
    ORDER BY f.id DESC
");

$logs_stmt->bind_param("i", $order_id);
$logs_stmt->execute();

$logs = $logs_stmt->get_result();
?>
<div class="container-fluid">

    <h2 class="fw-bold mb-4">
        Order Follow-Up #<?php echo $order_id; ?>
    </h2>

    <?php if(isset($_SESSION['success'])): ?>
        <div class="alert alert-success">
            <?php
            echo $_SESSION['success'];
            unset($_SESSION['success']);
            ?>
        </div>
    <?php endif; ?>

    <?php if(isset($error)): ?>
        <div class="alert alert-danger">
            <?php echo $error; ?>
        </div>
    <?php endif; ?>

    <div class="card shadow-sm p-4 mb-4">

        <!-- ORDER SUMMARY -->
        <h5>Order Summary</h5>

        <div class="row mb-4">

            <div class="col-md-3">
                <strong>Customer:</strong><br>
                <?php echo htmlspecialchars($order['name']); ?>
            </div>

            <div class="col-md-3">
                <strong>Phone:</strong><br>
                <?php echo htmlspecialchars($order['phone']); ?>
            </div>

            <div class="col-md-3">
                <strong>Courier:</strong><br>
                <?php echo htmlspecialchars($order['courier_name'] ?? 'N/A'); ?>
            </div>

            <div class="col-md-3">
                <strong>Status:</strong><br>

                <?php
                $status = strtolower($order['order_status']);

                if ($status == 'delivered') {
                    echo '<span class="badge bg-success">Delivered</span>';
                } elseif ($status == 'returned') {
                    echo '<span class="badge bg-danger">Returned</span>';
                } elseif ($status == 'dispatched') {
                    echo '<span class="badge bg-primary">In Transit</span>';
                } else {
                    echo htmlspecialchars($order['order_status']);
                }
                ?>
            </div>

        </div>

        <!-- FOLLOWUP FORM -->
        <form method="POST">

            <div class="row g-3">

                <div class="col-md-4">
                    <label>Follow-Up Status</label>

                    <select name="followup_status"
                            class="form-select"
                            required>

                        <option value="">Select Status</option>
                        <option value="In Transit">In Transit</option>
                        <option value="Delivered">Delivered</option>
                        <option value="Returned">Returned</option>

                    </select>
                </div>

                <div class="col-md-8">
                    <label>Remarks</label>

                    <input type="text"
                           name="remarks"
                           class="form-control"
                           placeholder="Customer received / courier delayed / returned due to address issue">
                </div>

            </div>

            <br>

            <button type="submit" class="btn btn-primary">
                Save Follow-Up
            </button>

            <a href="?page=manage_orders"
               class="btn btn-secondary">
               Back
            </a>

        </form>

    </div>

    <!-- FOLLOWUP LOGS -->
    <div class="card shadow-sm p-4">

        <h5>Follow-Up History</h5>

        <div class="table-responsive">

            <table class="table table-bordered table-striped">

                <thead class="table-dark">
                    <tr>
                        <th>ID</th>
                        <th>Status</th>
                        <th>Remarks</th>
                        <th>Updated By</th>
                        <th>Date & Time</th>
                    </tr>
                </thead>

                <tbody>

                    <?php if($logs->num_rows > 0): ?>

                        <?php while($log = $logs->fetch_assoc()): ?>

                            <tr>

                                <td>
                                    <?php echo $log['id']; ?>
                                </td>

                                <td>

                                    <?php
                                    $log_status = strtolower($log['followup_status']);

                                    if ($log_status == 'delivered') {
                                        echo '<span class="badge bg-success">Delivered</span>';
                                    } elseif ($log_status == 'returned') {
                                        echo '<span class="badge bg-danger">Returned</span>';
                                    } elseif ($log_status == 'in transit') {
                                        echo '<span class="badge bg-primary">In Transit</span>';
                                    } else {
                                        echo htmlspecialchars($log['followup_status']);
                                    }
                                    ?>

                                </td>

                                <td>
                                    <?php echo htmlspecialchars($log['remarks'] ?? 'N/A'); ?>
                                </td>

                                <td>
                                    <?php echo htmlspecialchars($log['username'] ?? 'System'); ?>
                                </td>

                                <td>
                                    <?php echo date("d-M-Y h:i A", strtotime($log['created_at'])); ?>
                                </td>

                            </tr>

                        <?php endwhile; ?>

                    <?php else: ?>

                        <tr>
                            <td colspan="5" class="text-center">
                                No follow-up history found.
                            </td>
                        </tr>

                    <?php endif; ?>

                </tbody>

            </table>

        </div>

    </div>

</div>

<?php include __DIR__ . '/../../../templates/footer.php'; ?>