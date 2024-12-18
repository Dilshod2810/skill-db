CREATE TABLE Users
(
    UserId    SERIAL PRIMARY KEY,
    FullName  VARCHAR(150),
    Email     VARCHAR(150) UNIQUE,
    Phone     VARCHAR(20),
    City      VARCHAR(100),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Skills
(
    SkillId     SERIAL PRIMARY KEY,
    UserId      INT,
    Title       VARCHAR(150),
    Description TEXT,
    CreatedAt   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users (UserId)
);

CREATE TABLE Requests
(
    RequestId        SERIAL PRIMARY KEY,
    FromUserId       INT,
    ToUserId         INT,
    RequestedSkillId INT,
    OfferedSkillId   INT,
    Status           VARCHAR(20) DEFAULT 'Pending',
    CreatedAt        TIMESTAMP   DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt        TIMESTAMP   DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (FromUserId) REFERENCES Users (UserId),
    FOREIGN KEY (ToUserId) REFERENCES Users (UserId),
    FOREIGN KEY (RequestedSkillId) REFERENCES Skills (SkillId),
    FOREIGN KEY (OfferedSkillId) REFERENCES Skills (SkillId)
);
