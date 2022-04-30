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
-- NOTE: Feed Analytics would use aggregate functions to record the
--       statistics of different Community Feeds
CREATE TABLE FeedAnalytics (
    feedAnalyticID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    feedID INT UNSIGNED NOT NULL,
    CONSTRAINT FeedAnalytics_PK PRIMARY KEY (feedAnalyticID),
    CONSTRAINT Feed_FeedA_FK FOREIGN KEY (feedID) REFERENCES Feed (feedID)
);
CREATE TABLE CommentAnalytics (
    commentAnalyticID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    commentID INT UNSIGNED NOT NULL,
    upvoteUsername VARCHAR(25) NOT NULL,
    deleteFlag TINYINT(1) NOT NULL DEFAULT 0,
    CONSTRAINT CommentAnalytics_PK PRIMARY KEY (commentAnalyticID),
    CONSTRAINT Comment_CommentA_FK FOREIGN KEY (commentID) REFERENCES Comment (commentID),
    CONSTRAINT Profile_UpvotePostA_FK FOREIGN KEY (upvoteUsername) REFERENCES Profile (username)
);
CREATE TABLE UpvotePostAnalytics (
    postID INT UNSIGNED NOT NULL,
    postTitle VARCHAR(75) NOT NULL,
    upvoteUsername VARCHAR(25) NOT NULL,
    isUpvote TINYINT(1) NOT NULL DEFAULT 1,
    CONSTRAINT UpvotePostAnalytics_PK PRIMARY KEY (postID, upvoteUsername),
    CONSTRAINT Post_FK FOREIGN KEY (postID, postTitle) REFERENCES Post (postID, postTitle),
    CONSTRAINT Profile_UpvotePostA_FK FOREIGN KEY (upvoteUsername) REFERENCES Profile (username)
);
CREATE TABLE UpvoteCommentAnalytics (
    commentID INT UNSIGNED NOT NULL,
    postID INT UNSIGNED NOT NULL,
    upvoteUsername VARCHAR(25) NOT NULL,
    isUpvote TINYINT(1) NOT NULL DEFAULT 1,
    CONSTRAINT UpvoteCommentAnalytics_PK PRIMARY KEY (commentID, upvoteUsername),
    CONSTRAINT Comment_UpvoteCommentA_FK FOREIGN KEY (commentID, postID) REFERENCES Comment (commentID, postID),
    CONSTRAINT Profile_UpvoteCommentA_FK FOREIGN KEY (upvoteUsername) REFERENCES Profile (username)
);
INSERT UpvotePostAnalytics (postID, postTitle, upvoteUsername)
VALUES(1, "test", "testuser") ON DUPLICATE KEY
UPDATE isUpvote = 1 + -1 * isUpvote;
-- DELETE FROM PostAnalytics
-- WHERE Delete_Flag = 1