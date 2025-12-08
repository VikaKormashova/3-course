DROP TABLE IF EXISTS subscription_raw CASCADE;

CREATE TABLE subscription_raw (
    id serial PRIMARY KEY,
    user_name text,
    plan text,
    price_cents integer,
    started_at timestamp DEFAULT now(),
    status text DEFAULT 'active',
    deprecated_flag boolean DEFAULT false
);

INSERT INTO subscription_raw (user_name, plan, price_cents, started_at, status, deprecated_flag) VALUES
('Alice Johnson', 'basic', 9900, now() - interval '360 days', 'active', false),
('Bob Smith', 'pro', 19900, now() - interval '330 days', 'active', false),
('Charlie Brown', 'business', 49900, now() - interval '300 days', 'paused', false),
('Diana Prince', 'enterprise', 99900, now() - interval '270 days', 'active', false),
('Evan Lee', 'basic', 9900, now() - interval '240 days', 'canceled', false),
('Fiona Adams', 'pro', 19900, now() - interval '210 days', 'active', false),
('George Miller', 'pro', 19900, now() - interval '200 days', 'active', false),
('Hannah Davis', 'business', 49900, now() - interval '195 days', 'active', false),
('Ian Wright', 'basic', 9900, now() - interval '180 days', 'paused', false),
('Julia Stone', 'enterprise', 99900, now() - interval '170 days', 'active', false),
('Kevin Park', 'business', 49900, now() - interval '165 days', 'active', false),
('Laura Chen', 'basic', 9900, now() - interval '160 days', 'active', false),
('Mark Green', 'pro', 19900, now() - interval '140 days', 'canceled', false),
('Nina Patel', 'business', 49900, now() - interval '120 days', 'active', false),
('Oscar Diaz', 'enterprise', 99900, now() - interval '110 days', 'active', false),
('Paula Gomez', 'pro', 19900, now() - interval '100 days', 'active', false),
('Quinn Baker', 'basic', 9900, now() - interval '90 days', 'active', false),
('Rita Ora', 'business', 49900, now() - interval '80 days', 'paused', false),
('Sam Young', 'pro', 19900, now() - interval '60 days', 'active', false),
('Tina King', 'enterprise', 99900, now() - interval '45 days', 'active', false),
('Uma Reed', 'basic', 9900, now() - interval '30 days', 'active', false),
('Victor Hugo', 'pro', 19900, now() - interval '20 days', 'active', false),
('Wendy Frost', 'business', 49900, now() - interval '10 days', 'active', false),
('Yara Novak', 'basic', 9900, now() - interval '5 days', 'canceled', false),
('Zack Cole', 'enterprise', 99900, now() - interval '2 days', 'active', false);

ALTER TABLE subscription_raw RENAME TO subscriptions;

ALTER TABLE subscriptions 
ALTER COLUMN price_cents TYPE NUMERIC(12,2) 
USING (price_cents / 100.0);

ALTER TABLE subscriptions 
RENAME COLUMN price_cents TO price;

ALTER TABLE subscriptions 
ADD COLUMN subscription_code TEXT;

UPDATE subscriptions 
SET subscription_code = CONCAT(
    'SUB-',
    UPPER(SUBSTRING(plan FROM 1 FOR 3)),
    '-',
    EXTRACT(YEAR FROM started_at)::TEXT,
    '-',
    LPAD(id::TEXT, 5, '0')
);

ALTER TABLE subscriptions 
ALTER COLUMN subscription_code SET NOT NULL;

ALTER TABLE subscriptions 
DROP COLUMN deprecated_flag;

SELECT 
    id,
    user_name,
    plan,
    price,
    subscription_code,
    started_at,
    status
FROM subscriptions
LIMIT 5;