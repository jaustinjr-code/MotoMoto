CREATE TABLE Authorization (
    featureName VARCHAR(255) NOT NULL,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Full_PK PRIMARY KEY (featureName, typeName)
);

INSERT INTO Authorization (featureName, typeName)
VALUES ('usageAnalysisDashboard', 'ADMIN');
