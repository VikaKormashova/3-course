DROP TABLE IF EXISTS bookings_import_lines;

CREATE TABLE bookings_import_lines (
  id SERIAL PRIMARY KEY,
  source_file TEXT NOT NULL,
  line_no INT NOT NULL,
  raw_line TEXT NOT NULL,
  imported_at TIMESTAMPTZ DEFAULT NOW(),
  note TEXT
);

INSERT INTO bookings_import_lines (source_file, line_no, raw_line, note) VALUES
('partnerA_2025_11.csv', 1, 'PNR: ABC123; Passenger: Ivan Petrov <ivan.petrov@example.com>; +7 (916) 111-22-33; Fare: "1,299.00" USD', 'booking row'),
('partnerA_2025_11.csv', 2, 'PNR: XYZ-999; Passenger: Maria; maria.s@mail.co; 8-916-1112233; Fare: "1 250,00" EUR', 'booking row'),
('partnerA_2025_11.csv', 3, 'PNR: BAD@@PNR; Passenger: Oops <bad@-domain.com>; +7 916 ABC-22-33; Fare: "999"', 'broken pnr and email'),
('carrier_feed.csv', 10, 'Voucher: VCHR-2025-0001; Flight: SU1234; seat: 12A', 'voucher row'),
('carrier_feed.csv', 11, 'Booking: pnr: qwe987; note: legacy', 'booking row'),
('dates_feed.csv', 1, 'checkin: 05/12/2025; checkout: 12/12/2025', 'dates'),
('dates_feed.csv', 2, 'checkin: 2025-12-05; checkout: 2025-12-12', 'iso dates'),
('dates_feed.csv', 3, 'checkin: Dec 5, 2025; checkout: Dec 12, 2025', 'text month'),
('flags.csv', 1, 'tags: vip,flex, refundable ', 'tags row'),
('flags.csv', 2, 'tags: economy, , promo', 'tags with possible empty'),
('passenger_dirty.csv', 5, '"Ivanov, Ivan","123 Red St, Apt 4","Passport: 123-45-678"', 'dirty csv'),
('passenger_dirty.csv', 6, '"Smith, Anna","Los Angeles, CA","Notes: needs wheelchair"', 'dirty csv'),
('import_log.txt', 300, 'INFO: started bookings import', 'log'),
('import_log.txt', 301, 'warning: missing fare for line 3', 'log'),
('import_log.txt', 302, 'error: failed to parse PNR for line 3', 'log'),
('import_log.txt', 303, 'Error: invalid date format in dates_feed.csv', 'log'),
('partnerA_2025_11.csv', 20, 'PNR: 123 456; Passenger: bad@@example..com; Fare: "1,2,99"', 'trap-bad-pnr-email-price'),
('flags.csv', 3, 'tags: vip,, ,flex', 'trap-empty-tags');

SELECT *
FROM bookings_import_lines
WHERE raw_line ~ '<?[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}>?';

SELECT *
FROM bookings_import_lines
WHERE raw_line !~ '<?[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}>?';

SELECT *
FROM bookings_import_lines
WHERE raw_line ~ '\b[A-Z]{2,}[A-Z0-9-]{2,}\b';

SELECT 
    id,
    source_file,
    line_no,
    COALESCE(
        (REGEXP_MATCH(raw_line, '<([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})>'))[1],
        (REGEXP_MATCH(raw_line, '([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})'))[1]
    ) AS email
FROM bookings_import_lines
WHERE raw_line ~ '[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}';

SELECT 
    id,
    source_file,
    line_no,
    REGEXP_MATCHES(raw_line, '\b(?:[A-Z]{2}\d{3,4}|VCHR-\d{4}-\d{4})\b', 'g') AS flight_or_voucher
FROM bookings_import_lines
WHERE raw_line ~ '\b(?:[A-Z]{2}\d{3,4}|VCHR-\d{4}-\d{4})\b';

SELECT 
    id,
    source_file,
    line_no,
    REGEXP_REPLACE(
        (REGEXP_MATCH(raw_line, '(\+?[78]\d[\d\s\(\)-]{8,})'))[1],
        '[^\d]', '', 'g'
    ) AS normalized_phone
FROM bookings_import_lines
WHERE raw_line ~ '\+?[78]\d[\d\s\(\)-]{8,}';

WITH fare_extract AS (
    SELECT 
        id,
        source_file,
        line_no,
        (REGEXP_MATCH(raw_line, 'Fare:\s*"([^"]+)"'))[1] AS fare_str
    FROM bookings_import_lines
    WHERE raw_line ~ 'Fare:\s*"'
)
SELECT 
    id,
    source_file,
    line_no,
    fare_str,
    CASE 
        WHEN fare_str ~ '^\d{1,3}(?:,\d{3})*\.\d{2}$' 
            THEN REPLACE(fare_str, ',', '')::NUMERIC
        WHEN fare_str ~ '^\d{1,3}(?: \d{3})*,\d{2}$' 
            THEN REPLACE(REPLACE(fare_str, ' ', ''), ',', '.')::NUMERIC
        WHEN fare_str ~ '^\d+$' 
            THEN fare_str::NUMERIC
        WHEN fare_str ~ '^\d+(?:[.,]\d+)+$' 
            THEN REPLACE(REGEXP_REPLACE(fare_str, '[^\d.]', '', 'g'), '..', '.')::NUMERIC
        ELSE NULL
    END AS normalized_price
FROM fare_extract;

SELECT 
    id,
    source_file,
    line_no,
    ARRAY_REMOVE(
        REGEXP_SPLIT_TO_ARRAY(
            REGEXP_REPLACE(raw_line, '^.*tags:\s*', ''),
            '\s*,\s*'
        ),
        ''
    ) AS tags_array
FROM bookings_import_lines
WHERE source_file = 'flags.csv';

WITH csv_cleaned AS (
    SELECT 
        id,
        line_no,
        REGEXP_REPLACE(raw_line, '^"|"$', '', 'g') AS cleaned_line
    FROM bookings_import_lines
    WHERE source_file = 'passenger_dirty.csv'
)
SELECT 
    id,
    line_no,
    ordinality AS field_num,
    REGEXP_REPLACE(field, '^"|"$', '', 'g') AS field_value
FROM csv_cleaned,
     REGEXP_SPLIT_TO_TABLE(cleaned_line, '","') WITH ORDINALITY AS field;

SELECT 
    id,
    source_file,
    line_no,
    raw_line,
    REGEXP_REPLACE(raw_line, 'error', 'ERROR', 'gi') AS normalized_log
FROM bookings_import_lines
WHERE source_file = 'import_log.txt'
  AND raw_line ~* 'error';