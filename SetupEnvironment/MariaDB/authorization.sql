CREATE TABLE Authorization (
    featureName VARCHAR(255) NOT NULL,
    typeName VARCHAR(25) NOT NULL,
    CONSTRAINT Feature_PK PRIMARY KEY (featureName)
);

INSERT INTO Authorization (featureName, typeName)
VALUES ('usageAnalysisDashboard', 'ADMIN');
