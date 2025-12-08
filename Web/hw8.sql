DROP TABLE IF EXISTS issues;

CREATE TABLE issues (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    component TEXT NOT NULL,
    severity INTEGER NOT NULL CHECK (severity BETWEEN 1 AND 10),
    status TEXT NOT NULL CHECK (status IN ('Open', 'In Progress', 'Resolved', 'Closed', 'Spam')),
    assignee TEXT,
    created_at DATE NOT NULL
);

INSERT INTO issues (title, component, severity, status, assignee, created_at) VALUES
('Crash during authentication', 'Auth', 8, 'Open', 'alex', '2023-03-15'),
('Crash on login page', 'AuthModule', 9, 'In Progress', 'maria', '2022-06-20'),
('Crash when token expires', 'Authentication', 10, 'Open', 'olga', '2024-12-31'),
('Crash with OAuth', 'OAuthService', 6, 'Closed', 'dmitry', '2022-01-01'),
('CRASH IN AUTH SERVICE', 'AUTH', 7, 'Resolved', 'ivan', '2023-05-15'),
('crash test auth', 'auth-backend', 5, 'Open', 'petr', '2023-07-10'),
('Error in authentication', 'Auth', 5, 'Open', 'anna', '2023-05-15'),
('Authentication failure', 'AuthSystem', 7, 'Resolved', 'pavel', '2023-07-22'),
('Crash in billing', 'Billing', 8, 'Open', 'sergey', '2021-12-31'),
('Crash on dashboard', 'Dashboard', 9, 'Closed', 'natalia', '2025-01-01'),
('High memory usage', 'Billing', 9, 'Open', 'viktor', '2023-08-10'),
('Database connection error', 'Database', 10, 'Open', 'ekaterina', '2023-09-05'),
('Security vulnerability', 'Security', 8, 'Open', 'artem', '2023-07-15'),
('Performance degradation', 'Performance', 7, 'Open', 'sofia', '2023-10-20'),
('Minor UI issue', 'UI', 6, 'Open', 'alexey', '2023-08-15'),
('System failure', 'Core', 10, 'Open', 'daria', '2023-09-10'),
('Test crash scenario', 'Testing', 9, 'Open', 'andrey', '2023-07-20'),
('Unit test failure', 'Tests', 8, 'Open', 'elena', '2023-08-25'),
('Resolved high severity', 'Auth', 9, 'Resolved', 'konstantin', '2023-07-30'),
('Demo spam ticket', 'Spam', 1, 'Spam', NULL, '2019-12-31'),
('Demo test spam', 'Testing', 2, 'Spam', NULL, '2018-06-15'),
('SPAM: Demo invitation', 'Marketing', 1, 'Spam', NULL, '2019-11-20'),
('demo spam lowercase', 'General', 1, 'Spam', NULL, '2019-10-10'),
('Demo but new', 'General', 1, 'Spam', NULL, '2020-01-01'),
('Demo but closed', 'Testing', 2, 'Closed', 'alex', '2019-05-15'),
('Regular spam', 'Spam', 1, 'Spam', NULL, '2019-08-20'),
('Spam demo ticket', 'Auth', 1, 'Spam', NULL, '2018-12-31');

SELECT id, title, component, severity, status, assignee, created_at
FROM issues
WHERE component ILIKE '%auth%'
  AND title ILIKE 'Crash%'
  AND created_at BETWEEN '2022-01-01' AND '2024-12-31'
ORDER BY created_at ASC, severity DESC;

UPDATE issues
SET status = 'In Progress'
WHERE status = 'Open'
  AND severity BETWEEN 7 AND 10
  AND title NOT ILIKE '%test%';

DELETE FROM issues
WHERE status ILIKE 'Spam'
  AND created_at < '2020-01-01'
  AND title ILIKE '%demo%';