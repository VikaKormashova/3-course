DROP TABLE IF EXISTS character_visit CASCADE;
DROP TABLE IF EXISTS character CASCADE;
DROP TABLE IF EXISTS location_info CASCADE;
DROP TABLE IF EXISTS location CASCADE;

CREATE TABLE location (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    region TEXT NOT NULL
);

CREATE TABLE location_info (
    location_id INTEGER PRIMARY KEY,
    danger_level INTEGER NOT NULL CHECK (danger_level BETWEEN 1 AND 10),
    climate TEXT NOT NULL,
    fast_travel_unlocked BOOLEAN NOT NULL DEFAULT FALSE,
    FOREIGN KEY (location_id) REFERENCES location(id) ON DELETE CASCADE
);

CREATE TABLE character (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    class TEXT NOT NULL,
    home_location_id INTEGER NOT NULL,
    FOREIGN KEY (home_location_id) REFERENCES location(id) ON DELETE RESTRICT
);

CREATE TABLE character_visit (
    character_id INTEGER NOT NULL,
    location_id INTEGER NOT NULL,
    visited_at TIMESTAMP NOT NULL,
    purpose TEXT NOT NULL,
    PRIMARY KEY (character_id, location_id, visited_at),
    FOREIGN KEY (character_id) REFERENCES character(id) ON DELETE CASCADE,
    FOREIGN KEY (location_id) REFERENCES location(id) ON DELETE CASCADE,
    CHECK (visited_at <= NOW())
);

INSERT INTO location (name, region) VALUES
('Dark Forest', 'Forest'),
('Frozen Peaks', 'Tundra');

INSERT INTO location_info VALUES
(1, 7, 'Temperate', TRUE),
(2, 9, 'Polar', FALSE);

INSERT INTO character (name, class, home_location_id) VALUES
('Aragorn', 'Warrior', 1),
('Gandalf', 'Mage', 2);

INSERT INTO character_visit VALUES
(1, 2, '2024-10-01 10:00:00', 'quest'),
(2, 1, '2024-10-02 14:30:00', 'trade');