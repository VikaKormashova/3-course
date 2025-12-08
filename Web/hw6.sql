CREATE SCHEMA fitness_center;

CREATE TABLE fitness_center.members (
    member_id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    registration_date DATE DEFAULT CURRENT_DATE
);

CREATE TABLE fitness_center.trainers (
    trainer_id INTEGER PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    specialization TEXT NOT NULL,
    experience_years INTEGER NOT NULL
);