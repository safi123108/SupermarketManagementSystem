<?php
include __DIR__ . '/../../../templates/auth_check.php';
include __DIR__ . '/../../../templates/header.php';
include __DIR__ . '/../../../templates/navbar.php';
include __DIR__ . '/../../../templates/sidebar.php';
require_once __DIR__ . '/../../../config/database.php';

/* =========================
   FILTER VALUES
========================= */
$from_date   = $_GET['from_date'] ?? '';
$to_date     = $_GET['to_date'] ?? '';
$status      = $_GET['status'] ?? '';
$source      = $_GET['source'] ?? '';
$phone       = $_GET['phone'] ?? '';
$order_id    = $_GET['order_id'] ?? '';
$followup_status = $_GET['followup_status'] ?? '';

/* =========================
   FILTER QUERY BUILD
========================= */
$where = [];

if (!empty($from_date)) {
    $where[] = "DATE(o.order_date) >= '" . $conn->real_escape_string($from_date) . "'";
}

if (!empty($to_date)) {
    $where[] = "DATE(o.order_date) <= '" . $conn->real_escape_string($to_date) . "'";
}

if (!empty($status)) {
    $where[] = "o.status = '" . $conn->real_escape_string($status) . "'";
}

if (!empty($source)) {
    $where[] = "o.order_source_id = '" . (int)$source . "'";
}

if (!empty($phone)) {
    $where[] = "c.phone LIKE '%" . $conn->real_escape_string($phone) . "%'";
}

if (!empty($order_id)) {
    $where[] = "o.id = '" . (int)$order_id . "'";
}
if (!empty($followup_status)) {
    $where[] = "latest_fu.followup_status = '" . $conn->real_escape_string($followup_status) . "'";
}

$where_sql = '';
if (count($where) > 0) {
    $where_sql = "WHERE " . implode(" AND ", $where);
}

/* =========================
   SOURCES FOR DROPDOWN
========================= */
$sources = $conn->query("SELECT id, source_name FROM order_sources WHERE status='Active' ORDER BY source_name ASC");

/* =========================
   MAIN QUERY
========================= */
$query = "
    SELECT 
        o.id,
        o.order_date,
        o.status,
        o.payment_status,
        o.total_amount,

        c.name AS customer_name,
        c.phone,

        os.source_name,

        COUNT(oi.id) AS total_items

  FROM orders o

LEFT JOIN (
    SELECT of1.order_id, of1.followup_status
    FROM order_followups of1
    INNER JOIN (
        SELECT order_id, MAX(created_at) AS latest_created
        FROM order_followups
        GROUP BY order_id
    ) of2
    ON of1.order_id = of2.order_id
    AND of1.created_at = of2.latest_created
) latest_fu
    ON o.id = latest_fu.order_id
";

$tile_counts = [];

$tile_statuses = [
    'total' => '',
    'pending' => 'pending',
    'placed' => 'placed',
    'confirmed' => 'confirmed',
    'hold' => 'hold',
    'cancelled' => 'cancelled',
    'ready_to_ship' => 'ready_to_ship',
    'dispatched' => 'dispatched'
];

foreach ($tile_statuses as $key => $st) {

    if ($st === '') {
        $tile_sql = "SELECT COUNT(*) as total FROM orders";
    } else {
        $tile_sql = "SELECT COUNT(*) as total FROM orders WHERE status = '$st'";
    }

    $tile_result = $conn->query($tile_sql);
    $tile_counts[$key] = $tile_result->fetch_assoc()['total'] ?? 0;
}

$result = $conn->query($query);
?>

<div class="container-fluid">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="fw-bold">Manage Orders</h2>

        <a href="?page=master_order" class="btn btn-primary">
            + Place New Order
        </a>
    </div>

    <?php if(isset($_SESSION['success'])): ?>
        <div class="alert alert-success">
            <?php
            echo $_SESSION['success'];
            unset($_SESSION['success']);
            ?>
        </div>
    <?php endif; ?>
<div class="row mb-4">

    <?php
    $tile_labels = [
        'total' => 'Total Orders',
        'pending' => 'Pending',
        'placed' => 'Placed',
        'confirmed' => 'Confirmed',
        'hold' => 'Hold',
        'cancelled' => 'Cancelled',
        'ready_to_ship' => 'Ready To Ship',
        'dispatched' => 'Dispatched'
    ];

    $tile_colors = [
        'total' => 'dark',
        'pending' => 'warning',
        'placed' => 'primary',
        'confirmed' => 'info',
        'hold' => 'secondary',
        'cancelled' => 'danger',
        'ready_to_ship' => 'success',
        'dispatched' => 'dark'
    ];

    foreach($tile_labels as $key => $label):
    ?>

        <div class="col-md-3 col-lg-3 mb-3">
            <a href="?page=manage_orders<?php echo ($key != 'total') ? '&status=' . $key : ''; ?>"
               class="text-decoration-none">

                <div class="card text-white bg-<?php echo $tile_colors[$key]; ?> shadow-sm">
                    <div class="card-body text-center">
                        <h6 class="mb-1"><?php echo $label; ?></h6>
                        <h3 class="fw-bold"><?php echo $tile_counts[$key]; ?></h3>
                    </div>
                </div>

            </a>
        </div>

    <?php endforeach; ?>

</div>
    <!-- FILTER CARD -->
    <div class="card shadow-sm p-3 mb-4">
        <form method="GET">

            <input type="hidden" name="page" value="manage_orders">

            <div class="row g-3">

                <div class="col-md-2">
                    <label class="form-label">From Date</label>
                    <input type="date" name="from_date" class="form-control"
                           value="<?php echo htmlspecialchars($from_date); ?>">
                </div>

                <div class="col-md-2">
                    <label class="form-label">To Date</label>
                    <input type="date" name="to_date" class="form-control"
                           value="<?php echo htmlspecialchars($to_date); ?>">
                </div>

                <div class="col-md-2">
                    <label class="form-label">Status</label>
                    <select name="status" class="form-select">
                        <option value="">All Status</option>
                        <?php
                        $statuses = ['pending','placed','confirmed','hold','cancelled','ready_to_ship','dispatched','delivered','returned'];
                        foreach($statuses as $st):
                        ?>
                            <option value="<?php echo $st; ?>" <?php if($status == $st) echo 'selected'; ?>>
                                <?php echo ucwords(str_replace('_', ' ', $st)); ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
<div class="col-md-2">
    <label class="form-label">Follow-up</label>
    <select name="followup_status" class="form-select">
        <option value="">All Follow-ups</option>

        <?php
        $followups = ['Call Pending','No Response','Callback','Converted','Closed'];
        foreach($followups as $fu):
        ?>
            <option value="<?php echo $fu; ?>" <?php if($followup_status == $fu) echo 'selected'; ?>>
                <?php echo $fu; ?>
            </option>
        <?php endforeach; ?>
    </select>
</div>
                <div class="col-md-2">
                    <label class="form-label">Source</label>
                    <select name="source" class="form-select">
                        <option value="">All Sources</option>
                        <?php while($src = $sources->fetch_assoc()): ?>
                            <option value="<?php echo $src['id']; ?>"
                                <?php if($source == $src['id']) echo 'selected'; ?>>
                                <?php echo htmlspecialchars($src['source_name']); ?>
                            </option>
                        <?php endwhile; ?>
                    </select>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Phone</label>
                    <input type="text" name="phone" class="form-control"
                           placeholder="Customer Phone"
                           value="<?php echo htmlspecialchars($phone); ?>">
                </div>

                <div class="col-md-2">
                    <label class="form-label">Order ID</label>
                    <input type="number" name="order_id" class="form-control"
                           placeholder="Order #"
                           value="<?php echo htmlspecialchars($order_id); ?>">
                </div>

                <div class="col-md-12 d-flex gap-2 mt-3">
                    <button type="submit" class="btn btn-success">
                        Apply Filters
                    </button>

                    <a href="?page=manage_orders" class="btn btn-secondary">
                        Reset
                    </a>
                </div>

            </div>

        </form>
    </div>

    <!-- TABLE -->
    <div class="card shadow-sm p-3">

        <div class="table-responsive">

            <table class="table table-bordered table-striped align-middle">

                <thead class="table-dark">
                    <tr>
                        <th>Order ID</th>
                        <th>Date</th>
                        <th>Customer</th>
                        <th>Phone</th>
                        <th>Source</th>
                        <th>Total Items</th>
                        <th>Grand Total</th>
                        <th>Order Status</th>
                        <th>Payment</th>
                        <th>Actions</th>
                    </tr>
                </thead>

                <tbody>

                    <?php if($result->num_rows > 0): ?>

                        <?php while($row = $result->fetch_assoc()): ?>

                            <tr>

                                <td>#<?php echo $row['id']; ?></td>

                                <td><?php echo date("d-M-Y", strtotime($row['order_date'])); ?></td>

                                <td><?php echo htmlspecialchars($row['customer_name'] ?? 'N/A'); ?></td>

                                <td><?php echo htmlspecialchars($row['phone'] ?? 'N/A'); ?></td>

                                <td><?php echo htmlspecialchars($row['source_name'] ?? 'N/A'); ?></td>

                                <td><?php echo $row['total_items']; ?></td>

                                <td>PKR <?php echo number_format($row['total_amount'], 2); ?></td>

                                <td>
                                    <?php
                                    $order_status = strtolower($row['status']);

                                    $badge = match($status) {
                                        'placed' => 'primary',
                                        'confirmed' => 'info',
                                        'hold' => 'warning',
                                        'cancelled' => 'danger',
                                        'ready_to_ship' => 'secondary',
                                        'dispatched' => 'dark',
                                        'delivered' => 'success',
                                        'returned' => 'dark',
                                        default => 'light'
                                    };

                                    echo '<span class="badge bg-' . $badge . '">' .
                                         ucwords(str_replace('_', ' ', $status)) .
                                         '</span>';
                                    ?>
                                </td>

                                <td>
                                    <?php if(strtolower($row['payment_status']) == 'paid'): ?>
                                        <span class="badge bg-success">Paid</span>
                                    <?php else: ?>
                                        <span class="badge bg-danger">Unpaid</span>
                                    <?php endif; ?>
                                </td>

                                <td class="d-flex gap-1">

                                    <a href="?page=edit_order&id=<?php echo $row['id']; ?>" class="btn btn-warning btn-sm">
    Edit
</a>

<?php if(
    in_array(
        strtolower($row['status']),
        ['confirmed', 'ready_to_ship', 'dispatched']
    )
): ?>

    <a href="?page=edit_order&id=<?php echo $row['id']; ?>" 
       class="btn btn-success btn-sm ms-1">
        Ship
    </a>

<?php endif; ?>

                                </td>

                            </tr>

                        <?php endwhile; ?>

                    <?php else: ?>

                        <tr>
                            <td colspan="10" class="text-center">
                                No orders found.
                            </td>
                        </tr>

                    <?php endif; ?>

                </tbody>

            </table>

        </div>

    </div>

</div>

<?php include __DIR__ . '/../../../templates/footer.php'; ?>