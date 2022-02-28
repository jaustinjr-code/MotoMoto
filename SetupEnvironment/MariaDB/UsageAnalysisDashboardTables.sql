-- NOTE: average duration is calculated at a higher level
-- recieve the current avg, update that avg then store the new avg
CREATE TABLE ViewAnalytics (
    viewID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    viewTitle VARCHAR(30) NOT NULL,
    displayTotal INT UNSIGNED DEFAULT 0,
    durationAvg INT UNSIGNED DEFAULT 0,
    CONSTRAINT ViewAnalytics_PK PRIMARY KEY (viewTitle)
);
-- NOTE: some MariaDB versions don't allow functions to be a default value for DATE
CREATE TABLE AdmissionAnalytics (
    accessID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    accessDate DATE NOT NULL DEFAULT CURRENT_DATE,
    loginTotal INT UNSIGNED DEFAULT 0,
    registrationTotal INT UNSIGNED DEFAULT 0,
    CONSTRAINT AdmissionAnalytics_PK PRIMARY KEY (accessDate)
);
CREATE TABLE CommunityBoardAnalytics (
    feedID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    feedTitle VARCHAR(20) NOT NULL,
    feedPostTotal INT UNSIGNED DEFAULT 0,
    CONSTRAINT CommunityBoardAnalytics_PK PRIMARY KEY (feedTitle)
);
-- NOTE: VARCHAR size for username is subject to change
-- NOTE: can possibly use foreign key for registration total and username
CREATE TABLE EventListAnalytics (
    eventID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    eventAccountUsername VARCHAR(30) NOT NULL,
    eventRegistrationTotal INT UNSIGNED DEFAULT 0,
    CONSTRAINT EventListAnalytics_PK PRIMARY KEY (eventAccountUsername)
);
-- ViewAnalytics values
INSERT INTO ViewAnalytics (viewTitle)
VALUES ("View"),
    ("View2");
UPDATE ViewAnalytics
SET displayTotal = displayTotal + 1;
UPDATE ViewAnalytics
SET displayTotal = displayTotal + 1,
    -- durationAvg = (durationAvg + [duration]) / displayTotal
    durationAvg = (durationAvg + 5) / displayTotal
WHERE viewTitle LIKE "View2";
SELECT displayTotal
FROM ViewAnalytics
ORDER BY displayTotal DESC
LIMIT 5;
SELECT durationAvg
FROM ViewAnalytics
ORDER BY durationAvg DESC
LIMIT 5;
-- AdmissionAnalytics values
INSERT INTO AdmissionAnalytics
VALUES ();
INSERT INTO AdmissionAnalytics (accessDate)
VALUES ("2022-2-2");
UPDATE AdmissionAnalytics
SET loginTotal = loginTotal + 1;
UPDATE AdmissionAnalytics
SET registrationTotal = registrationTotal + 1;
SELECT *
FROM AdmissionAnalytics;
-- CommunityBoardAnalytics values
INSERT INTO CommunityBoardAnalytics (feedTitle, feedPostTotal)
VALUES ("Supercar", 20);
UPDATE CommunityBoardAnalytics
SET feedPostTotal = feedPostTotal + 20 -- SET feedPostTotal = feedPostTotal + [total new posts in database]
    -- NOTE: can link to Community Board table to update this value w/ foreign key
WHERE feedTitle LIKE "Supercar";
-- WHERE feedTitle LIKE [community title passed in upon entry]
-- NOTE: must guarantee community title is valid, maybe query Community Board table to see if it exists
-- EventListAnalytics values
INSERT INTO EventListAnalytics (eventAccountUsername)
VALUES ("jacobCrib");
UPDATE EventListAnalytics
SET eventRegistrationTotal = eventRegistrationTotal + 1
WHERE eventAccountUsername LIKE "jacobCrib";
-- SET eventRegistrationTotal = eventRegistrationTotal + [reg total when event begins and is removed];
SELECT *
FROM CommunityBoardAnalytics;