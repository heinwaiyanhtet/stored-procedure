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
