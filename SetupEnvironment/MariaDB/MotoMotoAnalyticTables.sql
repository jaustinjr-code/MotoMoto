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