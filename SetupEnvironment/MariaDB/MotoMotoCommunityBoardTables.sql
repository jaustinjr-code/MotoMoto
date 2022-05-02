CREATE TABLE Comment (
    commentID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    postID INT UNSIGNED NOT NULL,
    commentUsername VARCHAR(25) NOT NULL,
    commentDescription VARCHAR(1000) NOT NULL,
    submitUTC TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Comment_PK PRIMARY KEY (commentID, postID),
    CONSTRAINT Profile_Comment_FK FOREIGN KEY (commentUsername) REFERENCES Profile (username),
    CONSTRAINT Post_Comment_FK FOREIGN KEY (postID) REFERENCES Post (postID)
);
CREATE TABLE Post (
    postID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    postUsername VARCHAR(25) NOT NULL,
    feedName VARCHAR(25) NOT NULL,
    postTitle VARCHAR(75) NOT NULL,
    postDescription VARCHAR(1500) NOT NULL,
    -- imageID INT UNSIGNED NOT NULL,
    submitUTC TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT Post_PK PRIMARY KEY (postID, postTitle),
    -- CONSTRAINT Image_FK FOREIGN KEY (imageID) REFERENCES Image (imageID),
    CONSTRAINT Profile_Post_FK FOREIGN KEY (postUsername) REFERENCES Profile (username),
    CONSTRAINT Feed_Post_FK FOREIGN KEY (feedName) REFERENCES Feed (feedName)
);
CREATE TABLE Image (
    postID INT UNSIGNED NOT NULL,
    imageID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    imagePath VARCHAR(100) NOT NULL,
    CONSTRAINT Image_PK PRIMARY KEY (imageID),
    CONSTRAINT Post_Image_FK FOREIGN KEY (postID) REFERENCES Post (postID)
);
CREATE TABLE Feed (
    feedID INT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE,
    feedName VARCHAR(25) NOT NULL UNIQUE,
    CONSTRAINT Feed_PK PRIMARY KEY (feedName)
);
-- Feed dummy data
INSERT INTO Feed
VALUES (NULL, 'test');
-- Post dummy data
INSERT INTO Post
VALUES (null, 'testuser', 'test', 'test', 'test', NULL);
-- Image dummy data
INSERT INTO Image
VALUES (1, NULL, '/Desktop/');