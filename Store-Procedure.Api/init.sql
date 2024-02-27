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




















