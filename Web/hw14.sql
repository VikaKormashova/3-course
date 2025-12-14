DROP TABLE IF EXISTS test_products;

CREATE TABLE test_products (
    id SERIAL PRIMARY KEY,
    product_name TEXT NOT NULL,
    sku TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO test_products (product_name, sku, description)
SELECT 
    'Product ' || s.id,
    'SKU-' || LPAD(FLOOR(RANDOM() * 1000000)::TEXT, 6, '0'),
    'This is a product description with special pattern ' || 
        CASE WHEN RANDOM() < 0.3 THEN 'premium ' ELSE '' END ||
        CASE WHEN RANDOM() < 0.3 THEN 'quality ' ELSE '' END ||
        CASE WHEN RANDOM() < 0.3 THEN 'unique ' ELSE '' END ||
        'item number ' || s.id || ' created for testing purposes.'
FROM generate_series(1, 1000000) AS s(id);

SELECT 'Количество записей в таблице: ' || COUNT(*) FROM test_products;

SELECT * FROM test_products WHERE sku = 'SKU-123456';

CREATE INDEX idx_test_products_sku ON test_products(sku);

SELECT * FROM test_products WHERE sku = 'SKU-123456';

SELECT * FROM test_products WHERE description ILIKE '%pattern%';

CREATE EXTENSION IF NOT EXISTS pg_trgm;

CREATE INDEX idx_test_products_description_lower_trgm 
ON test_products USING gin (lower(description) gin_trgm_ops);

SELECT * FROM test_products WHERE lower(description) LIKE '%pattern%';