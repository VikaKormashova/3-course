DROP TABLE IF EXISTS deliveries;
CREATE TABLE deliveries (
  delivery_id     SERIAL PRIMARY KEY,
  delivery_date   DATE         NOT NULL,
  city            TEXT         NOT NULL CHECK(city IN ('Stockholm', 'Gothenburg', 'Malmo', 'Uppsala')),
  platform        TEXT         NOT NULL CHECK(platform IN ('ios', 'android', 'web')),
  cuisine         TEXT         NOT NULL CHECK(cuisine IN ('pizza', 'sushi', 'burger', 'salad', 'indian')),
  payment_method  TEXT         NOT NULL CHECK(payment_method IN ('card', 'cash', 'wallet')),
  status          TEXT         NOT NULL CHECK(status IN ('placed', 'accepted', 'delivered', 'cancelled', 'refunded')),
  items_count     INT          NOT NULL,
  subtotal        NUMERIC(7,2) NOT NULL,
  delivery_fee    NUMERIC(6,2) NOT NULL,
  tip             NUMERIC(6,2) NOT NULL DEFAULT 0,
  discount        NUMERIC(6,2) NOT NULL DEFAULT 0
);

INSERT INTO deliveries (delivery_date,city,platform,cuisine,payment_method,status,items_count,subtotal,delivery_fee,tip,discount) VALUES
('2025-01-03','Stockholm','ios','pizza','card','delivered',      2, 220,  39, 20,  0),
('2025-01-04','Stockholm','android','sushi','wallet','delivered',3, 275,  29,  0, 25),
('2025-01-05','Stockholm','web','burger','card','cancelled',     1, 120,  19,  0,  0),
('2025-01-07','Stockholm','ios','salad','cash','delivered',      2, 160,  29, 10,  0),
('2025-01-10','Stockholm','web','sushi','card','refunded',       4, 340,  39,  0, 40),
('2025-02-02','Gothenburg','ios','burger','card','delivered',    3, 240,  29, 15,  0),
('2025-02-03','Gothenburg','android','pizza','wallet','delivered',2, 185,  29,  0, 10),
('2025-02-04','Gothenburg','web','indian','card','delivered',    4, 320,  39,  0,  0),
('2025-02-06','Gothenburg','ios','salad','cash','cancelled',     1,  95,  19,  0,  0),
('2025-02-09','Gothenburg','android','sushi','card','delivered', 2, 210,  29,  8,  0),
('2025-03-01','Malmo','web','pizza','wallet','delivered',        3, 265,  39,  5,  0),
('2025-03-02','Malmo','ios','burger','card','delivered',         2, 190,  29,  0,  0),
('2025-03-03','Malmo','android','sushi','cash','delivered',      4, 330,  39,  0, 20),
('2025-03-05','Malmo','web','salad','card','cancelled',          1,  85,  19,  0,  0),
('2025-03-07','Malmo','ios','indian','wallet','refunded',        3, 280,  29,  0,  0),
('2025-03-10','Uppsala','ios','pizza','card','delivered',        2, 210,  29,  6,  0),
('2025-03-11','Uppsala','android','burger','cash','delivered',   3, 235,  29,  0, 15),
('2025-03-12','Uppsala','web','sushi','card','delivered',        4, 345,  39,  0,  0),
('2025-03-14','Uppsala','ios','salad','wallet','cancelled',      1,  92,  19,  0,  0),
('2025-03-16','Uppsala','android','indian','card','delivered',   2, 195,  29,  4,  0),
('2025-04-01','Stockholm','web','pizza','card','delivered',      4, 360,  39,  0,  0),
('2025-04-02','Stockholm','ios','sushi','wallet','delivered',    2, 225,  29, 12,  0),
('2025-04-03','Stockholm','android','burger','card','delivered', 3, 255,  29,  0, 20),
('2025-04-04','Gothenburg','web','indian','wallet','refunded',   2, 210,  29,  0,  0),
('2025-04-06','Gothenburg','ios','pizza','card','delivered',     2, 200,  29,  7,  0),
('2025-04-07','Malmo','android','salad','card','delivered',      2, 170,  29,  0, 10),
('2025-04-08','Malmo','web','burger','cash','delivered',         3, 245,  39,  3,  0),
('2025-04-09','Uppsala','ios','sushi','card','cancelled',        1, 110,  19,  0,  0);

SELECT 
    delivery_id,
    city,
    status,
    CASE 
        WHEN status = 'delivered' 
        THEN subtotal - discount + delivery_fee + tip 
        ELSE 0 
    END AS net_revenue,
    CASE 
        WHEN subtotal <= 200 THEN 'low'
        WHEN subtotal BETWEEN 201 AND 300 THEN 'mid'
        ELSE 'high'
    END AS ticket_band
FROM deliveries
ORDER BY delivery_date, delivery_id;

SELECT 
    delivery_id,
    platform,
    subtotal
FROM deliveries
WHERE subtotal > CASE 
    WHEN platform = 'ios' THEN 200
    WHEN platform = 'android' THEN 220
    ELSE 250
END
ORDER BY delivery_id;

SELECT 
    city,
    COUNT(*) AS orders_cnt,
    SUM(subtotal) AS sum_subtotal,
    AVG(delivery_fee) AS avg_fee,
    MAX(tip) AS max_tip
FROM deliveries
GROUP BY city
ORDER BY sum_subtotal DESC;

SELECT 
    city,
    SUM(subtotal - discount + delivery_fee + tip) 
        FILTER (WHERE status = 'delivered') AS delivered_revenue,
    COUNT(*) 
        FILTER (WHERE status = 'refunded') AS refund_cnt,
    COUNT(*) 
        FILTER (WHERE status = 'cancelled')::NUMERIC / COUNT(*) AS cancel_rate
FROM deliveries
GROUP BY city
ORDER BY cancel_rate DESC;

SELECT 
    cuisine,
    COUNT(*) AS total_orders,
    COUNT(*) 
        FILTER (WHERE status = 'cancelled') AS cancelled_orders,
    SUM(subtotal - discount + delivery_fee + tip) 
        FILTER (WHERE status = 'delivered') AS delivered_revenue
FROM deliveries
GROUP BY cuisine
HAVING 
    COUNT(*) FILTER (WHERE status = 'cancelled')::NUMERIC / COUNT(*) > 0.12
    OR SUM(subtotal - discount + delivery_fee + tip) 
        FILTER (WHERE status = 'delivered') > 1000
ORDER BY cuisine;