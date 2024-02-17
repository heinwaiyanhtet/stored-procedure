create table authors(
	id INT AUTO_INCREMENT PRIMARY KEY,
	authorName varchar(255) NOT NULL,
	birthdayName DATE null,
	bio text,
	createdAt timestamp default CURRENT_TIMESTAMP,
	updatedAt timestamp default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE books (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255),
    author_id INT,
    description text,
    image text,
    FOREIGN KEY (author_id) REFERENCES authors(id)
);

CREATE TABLE readers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    dateOfBirth DATE,
    email VARCHAR(255) null,
    address VARCHAR(255) null,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE borrowings (
    id INT AUTO_INCREMENT PRIMARY KEY,
    readerId INT,
    bookId INT,
    borrowDate DATE,
    returnDate DATE,
    FOREIGN KEY (readerId) REFERENCES readers(id),
    FOREIGN KEY (bookId) REFERENCES books(id)
);

CREATE TABLE genres (
    id INT AUTO_INCREMENT PRIMARY KEY,
    genreName VARCHAR(255) NOT NULL,
    createdAt timestamp default CURRENT_TIMESTAMP,
	updatedAt timestamp default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE book_genres (
    id INT AUTO_INCREMENT PRIMARY KEY,
    book_id INT,
    genre_id INT,
    FOREIGN KEY (book_id) REFERENCES books(id),
    FOREIGN KEY (genre_id) REFERENCES genres(id)
);


INSERT INTO `authors` 
(`id`, `authorName`, `birthdayName`, `bio`, `createdAt`, `updatedAt`) VALUES (1, 'hein wai yan htet', '2017-06-15', 'hell bio', '2017-06-15 00:00:00', '2017-06-15 00:00:00');


DELIMITER //
CREATE PROCEDURE GetAuthors()
BEGIN
    SELECT * FROM authors;
END //
DELIMITER ;



DELIMITER //
CREATE PROCEDURE GetAuthorById(IN authorId INT)
BEGIN
    SELECT * FROM authors WHERE id = authorId;
END //
DELIMITER ;


DELIMITER //

CREATE PROCEDURE AUTHORCRUD(
    IN p_Id INT,
    IN p_authorName varchar(255),
    In p_birthdayName DATE,
    In p_bio varchar(255),
    In p_created_at DATE,
    IN p_updated_at DATE,
    IN p_statementType VARCHAR(20)
)
BEGIN
    IF p_statementType = 'Insert' THEN
        INSERT INTO authors(Id, AuthorName, BirthdayName, Bio, CreatedAt, UpdatedAt)
        VALUES (p_Id, p_authorName, p_birthdayName, p_bio, p_created_at, p_updated_at);
        
    ELSEIF p_statementType = 'SELECT' THEN
        SELECT * FROM authors;

    ELSEIF p_statementType = 'UPDATE' THEN
        UPDATE authors
        SET
            AuthorName = p_authorName,
            BirthdayName = p_birthdayName,
            Bio = p_bio,
            CreatedAt = p_created_at,
            UpdatedAt = p_updated_at
        WHERE Id = p_Id;

    ELSEIF p_statementType = 'DELETE' THEN
        DELETE FROM authors WHERE Id = p_Id;
    
     ELSEIF p_statementType = 'GETBYID' THEN
        SELECT * FROM authors WHERE Id = p_Id;

    END IF;
END //

DELIMITER ;













