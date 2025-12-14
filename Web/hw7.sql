CREATE TABLE flights (
    id INTEGER PRIMARY KEY,
    flight_code CHAR(6) NOT NULL,
    duration_minutes INTEGER NOT NULL,
    ticket_price NUMERIC(10, 2) NOT NULL,
    
    CONSTRAINT chk_flight_code_format 
        CHECK (flight_code ~ '^SU[0-9]{4}$'),
    CONSTRAINT chk_duration_range 
        CHECK (duration_minutes BETWEEN 30 AND 600),
    CONSTRAINT chk_ticket_price 
        CHECK (ticket_price > 100)
);

INSERT INTO flights (id, flight_code, duration_minutes, ticket_price) VALUES
(1, 'SU1234', 180, 150.50),
(2, 'SU5678', 90, 120.00),
(3, 'SU9012', 360, 250.75);