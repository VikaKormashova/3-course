-- ==============================================
-- RPG SAMPLE — DDL + SEED
-- ==============================================

-- Очистка
DROP TABLE IF EXISTS loan CASCADE;
DROP TABLE IF EXISTS item_location CASCADE;
DROP TABLE IF EXISTS character_item CASCADE;
DROP TABLE IF EXISTS player_guild CASCADE;
DROP TABLE IF EXISTS item CASCADE;
DROP TABLE IF EXISTS "character" CASCADE;
DROP TABLE IF EXISTS guild CASCADE;
DROP TABLE IF EXISTS location CASCADE;
DROP TABLE IF EXISTS player CASCADE;

-- Игроки (аккаунты)
CREATE TABLE player (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL UNIQUE,
    email TEXT
);

-- Гильдии
CREATE TABLE guild (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    founded_year INT
);

-- MtM: player <-> guild с атрибутом member_tag
CREATE TABLE player_guild (
    guild_id INT NOT NULL REFERENCES guild(id) ON DELETE CASCADE,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    member_tag TEXT NOT NULL,
    joined_at DATE DEFAULT CURRENT_DATE,
    PRIMARY KEY (guild_id, player_id),
    CONSTRAINT uniq_tag_per_guild UNIQUE (guild_id, member_tag)
);

-- Персонажи
CREATE TABLE "character" (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    class TEXT,
    level INT DEFAULT 1
);

-- Предметы
CREATE TABLE item (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    item_type TEXT,
    rarity TEXT,
    description TEXT
);

-- MtM: character <-> item (инвентарь) с quantity, equipped
CREATE TABLE character_item (
    character_id INT NOT NULL REFERENCES "character"(id) ON DELETE CASCADE,
    item_id INT NOT NULL REFERENCES item(id) ON DELETE CASCADE,
    quantity INT NOT NULL CHECK (quantity >= 0),
    equipped BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (character_id, item_id)
);

-- Локации (города / посты)
CREATE TABLE location (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    region TEXT,
    level_requirement INT
);

-- MtM: item <-> location с quantity (на складе/в продаже)
CREATE TABLE item_location (
    location_id INT NOT NULL REFERENCES location(id) ON DELETE CASCADE,
    item_id INT NOT NULL REFERENCES item(id) ON DELETE CASCADE,
    quantity INT NOT NULL CHECK (quantity >= 0),
    vendor_name TEXT,
    PRIMARY KEY (location_id, item_id)
);

-- Loan: временная передача/аренда предмета из локации игроку
CREATE TABLE loan (
    id SERIAL PRIMARY KEY,
    location_id INT NOT NULL REFERENCES location(id) ON DELETE CASCADE,
    guild_id INT,
    member_tag TEXT,
    player_id INT NOT NULL REFERENCES player(id) ON DELETE CASCADE,
    item_id INT NOT NULL REFERENCES item(id) ON DELETE RESTRICT,
    loan_days INT NOT NULL CHECK (loan_days > 0),
    loaned_at DATE NOT NULL DEFAULT CURRENT_DATE,
    returned_at DATE,
    CONSTRAINT fk_player_guild_pair FOREIGN KEY (guild_id, member_tag)
        REFERENCES player_guild (guild_id, member_tag)
        ON DELETE RESTRICT
);

CREATE INDEX idx_loan_player ON loan(player_id);
CREATE INDEX idx_loan_item ON loan(item_id);

-- =========================
-- SEED DATA
-- =========================

-- Players
INSERT INTO player (id, username, email) VALUES
(1, 'hero_ivan', 'ivan@example.com'),
(2, 'mage_maria', 'maria@example.com'),
(3, 'rogue_oleg', 'oleg@example.com'),
(4, 'newbie', NULL),
(5, 'loner', 'solo@example.com');

SELECT setval(pg_get_serial_sequence('player','id'), (SELECT MAX(id) FROM player));

-- Guilds
INSERT INTO guild (id, name, founded_year) VALUES
(1, 'Order of Dawn', 2010),
(2, 'Shadow Syndicate', 2018),
(3, 'Wandering Minstrels', NULL);

SELECT setval(pg_get_serial_sequence('guild','id'), (SELECT MAX(id) FROM guild));

-- Player-Guild memberships
INSERT INTO player_guild (guild_id, player_id, member_tag, joined_at) VALUES
(1, 1, 'D-1001', '2020-01-15'),
(1, 2, 'D-1002', '2021-03-20'),
(2, 3, 'S-3001', '2022-07-07'),
(1, 5, 'D-1005', '2024-05-05');

-- Characters
INSERT INTO "character" (id, name, player_id, class, level) VALUES
(1, 'Ivan the Brave', 1, 'Warrior', 35),
(2, 'Maria the Wise', 2, 'Mage', 40),
(3, 'Oleg Shadowstep', 3, 'Rogue', 27),
(4, 'Novice Pete', 4, 'Peasant', 2),
(5, 'Solo Wanderer', 5, 'Ranger', 18);

SELECT setval(pg_get_serial_sequence('character','id'), (SELECT MAX(id) FROM "character"));

-- Items
INSERT INTO item (id, name, item_type, rarity, description) VALUES
(1, 'Sword of Dawn', 'Weapon', 'Epic', 'Ancient blade of the Dawn.'),
(2, 'Common Potion', 'Consumable', 'Common', 'Restores 50 HP.'),
(3, 'Shadow Dagger', 'Weapon', 'Rare', 'Favored by assassins.'),
(4, 'Traveler Cloak', 'Armor', 'Uncommon', 'Light cloak for travelers.'),
(5, 'Ancient Tome', 'Misc', 'Legendary', 'Contains forbidden knowledge.'),
(6, 'Wooden Shield', 'Armor', 'Common', 'Basic protection.'),
(7, 'Boots of Swiftness', 'Armor', 'Rare', 'Increases speed.');

SELECT setval(pg_get_serial_sequence('item','id'), (SELECT MAX(id) FROM item));

-- Character inventory (character_item)
INSERT INTO character_item (character_id, item_id, quantity, equipped) VALUES
(1, 1, 1, TRUE),
(1, 6, 1, TRUE),
(2, 2, 5, FALSE),
(2, 5, 1, FALSE),
(3, 3, 1, TRUE),
(5, 4, 1, TRUE);

-- Locations
INSERT INTO location (id, name, region, level_requirement) VALUES
(1, 'Sunhold', 'Northern Plains', 1),
(2, 'Shadowport', 'Coastlands', 10),
(3, 'Magehaven', 'Highlands', 20),
(4, 'Old Ruins', 'Badlands', 15),
(5, 'Lonely Inn', 'Crossroads', NULL);

SELECT setval(pg_get_serial_sequence('location','id'), (SELECT MAX(id) FROM location));

-- Item availability in locations (item_location)
INSERT INTO item_location (location_id, item_id, quantity, vendor_name) VALUES
(1, 2, 50, 'Alchemist Borya'),
(1, 6, 10, 'Blacksmith Tor'),
(2, 3, 2, 'Shadow Vendor'),
(2, 7, 1, 'Bootmaster'),
(3, 5, 1, 'Archivist'),
(4, 4, 3, 'Rogue Market'),
(5, 2, 10, 'Innkeeper');

-- Loans
INSERT INTO loan (location_id, guild_id, member_tag, player_id, item_id, loan_days, loaned_at) VALUES
(1, 1, 'D-1001', 1, 2, 7, '2025-11-01'),
(3, NULL, NULL, 2, 5, 30, '2025-10-15'),
(2, 2, 'S-3001', 3, 3, 14, '2025-11-05'),
(5, 1, 'D-1005', 5, 2, 3, '2025-11-10');

-- ==============================================
-- ЗАДАНИЯ ПО JOIN
-- ==============================================

-- 1. INNER JOIN
SELECT 
    c.name AS character_name,
    c.class,
    c.level,
    p.username AS player_username
FROM "character" c
INNER JOIN player p ON c.player_id = p.id
ORDER BY c.level DESC;

SELECT 
    l.name AS location_name,
    i.name AS item_name,
    il.quantity,
    il.vendor_name
FROM location l
INNER JOIN item_location il ON l.id = il.location_id
INNER JOIN item i ON il.item_id = i.id
WHERE il.quantity > 0
ORDER BY l.name, i.name;

-- 2. LEFT JOIN

SELECT 
    c.name AS character_name,
    c.class,
    i.name AS item_name,
    ci.quantity AS item_quantity,
    ci.equipped
FROM "character" c
LEFT JOIN character_item ci ON c.id = ci.character_id
LEFT JOIN item i ON ci.item_id = i.id
ORDER BY c.name, i.name;

SELECT 
    p.username,
    p.email,
    COUNT(c.id) AS character_count
FROM player p
LEFT JOIN "character" c ON p.id = c.player_id
GROUP BY p.id, p.username, p.email
ORDER BY character_count DESC, p.username;

-- 3. RIGHT JOIN

SELECT 
    l.name AS location_name,
    l.region,
    i.name AS item_name,
    il.quantity,
    il.vendor_name
FROM item_location il
RIGHT JOIN location l ON il.location_id = l.id
LEFT JOIN item i ON il.item_id = i.id
ORDER BY l.name, il.quantity DESC NULLS LAST;

SELECT 
    i.item_type,
    COUNT(DISTINCT il.location_id) AS location_count
FROM item_location il
RIGHT JOIN item i ON il.item_id = i.id AND il.quantity > 0
GROUP BY i.item_type
ORDER BY location_count DESC, item_type;

-- 4. FULL JOIN

SELECT 
    i.id AS item_id,
    i.name AS item_name,
    i.item_type,
    il.location_id,
    l.name AS location_name,
    il.quantity,
    il.vendor_name
FROM item i
FULL JOIN item_location il ON i.id = il.item_id
LEFT JOIN location l ON il.location_id = l.id
ORDER BY i.id NULLS LAST, il.location_id;

SELECT 
    p.id AS player_id,
    p.username,
    g.id AS guild_id,
    g.name AS guild_name,
    pg.member_tag,
    pg.joined_at
FROM player p
FULL JOIN player_guild pg ON p.id = pg.player_id
FULL JOIN guild g ON pg.guild_id = g.id
ORDER BY guild_name NULLS FIRST, username;

-- 5. CROSS JOIN

SELECT 
    l.name AS location_name,
    l.region,
    it.item_type
FROM location l
CROSS JOIN (SELECT DISTINCT item_type FROM item WHERE item_type IS NOT NULL) it
ORDER BY l.name, it.item_type;

SELECT 
    c.name AS character_name,
    c.class,
    l.name AS location_name,
    l.region
FROM "character" c
CROSS JOIN location l
ORDER BY c.name, l.name
LIMIT 100;

-- 6. LATERAL JOIN

SELECT 
    l.id AS location_id,
    l.name AS location_name,
    top_item.item_id,
    i.name AS item_name,
    top_item.max_quantity
FROM location l
LEFT JOIN LATERAL (
    SELECT 
        il.item_id,
        il.quantity AS max_quantity
    FROM item_location il
    WHERE il.location_id = l.id
    ORDER BY il.quantity DESC
    LIMIT 1
) AS top_item ON TRUE
LEFT JOIN item i ON top_item.item_id = i.id
ORDER BY l.name;

SELECT 
    p.id AS player_id,
    p.username,
    last_loan.loan_id,
    last_loan.item_id,
    i.name AS item_name,
    last_loan.loaned_at,
    last_loan.loan_days
FROM player p
LEFT JOIN LATERAL (
    SELECT 
        l.id AS loan_id,
        l.item_id,
        l.loaned_at,
        l.loan_days
    FROM loan l
    WHERE l.player_id = p.id
    ORDER BY l.loaned_at DESC
    LIMIT 1
) AS last_loan ON TRUE
LEFT JOIN item i ON last_loan.item_id = i.id
ORDER BY p.username;

-- 7. SELF JOIN

SELECT 
    c1.name AS character1_name,
    c1.class AS character1_class,
    c2.name AS character2_name,
    c2.class AS character2_class,
    p.username AS player_username
FROM "character" c1
INNER JOIN "character" c2 
    ON c1.player_id = c2.player_id 
    AND c1.id < c2.id
INNER JOIN player p ON c1.player_id = p.id
ORDER BY p.username, c1.name, c2.name;

SELECT DISTINCT
    i1.name AS item1_name,
    i1.item_type AS item1_type,
    i1.rarity,
    i2.name AS item2_name,
    i2.item_type AS item2_type
FROM item i1
INNER JOIN item i2 
    ON i1.rarity = i2.rarity 
    AND i1.id < i2.id
WHERE i1.rarity IS NOT NULL
ORDER BY i1.rarity, i1.name, i2.name;