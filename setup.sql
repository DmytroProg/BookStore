insert [GENRE] values ('Adventure'), ('Classics'), ('Comic'), ('Graphic Novel'), ('Detective'),
('Mistery'), ('Fantasy'), ('Romance'), ('Horror'), ('Fiction'), ('Sci-Fi'), ('Thriller');

insert [AUTHOR] values ('Joanne', 'Rowling', 1965), ('Stephen', 'King', 1947), 
('Li', 'Harper', 1926);

insert [BOOK] values ('Harry Potter. Philosopher`s Stone', 1, 'Bloomsbury', 232, 1997, 10, 15, 1, 'https://upload.wikimedia.org/wikipedia/en/thumb/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg/220px-Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg', NULL);
insert [BOOK] values ('Harry Potter. Chamber of Secrets', 1, 'Bloomsbury', 345, 1998, 14, 20, 2, 'https://upload.wikimedia.org/wikipedia/en/5/5c/Harry_Potter_and_the_Chamber_of_Secrets.jpg', NULL);
insert [BOOK] values ('Harry Potter. Prisoner of Azkaban', 1, 'Bloomsbury', 382, 1999, 18, 25, 3, 'https://upload.wikimedia.org/wikipedia/en/thumb/a/a0/Harry_Potter_and_the_Prisoner_of_Azkaban.jpg/220px-Harry_Potter_and_the_Prisoner_of_Azkaban.jpg', NULL);
insert [BOOK] values ('Harry Potter. Goblet of Fire', 1, 'Bloomsbury', 670, 2000, 28, 35, 4, 'https://upload.wikimedia.org/wikipedia/en/b/b6/Harry_Potter_and_the_Goblet_of_Fire_cover.png', NULL);
insert [BOOK] values ('Harry Potter. Order of the Phoenix', 1, 'Bloomsbury', 813, 2003, 36, 40, 5, 'https://upload.wikimedia.org/wikipedia/en/thumb/7/70/Harry_Potter_and_the_Order_of_the_Phoenix.jpg/220px-Harry_Potter_and_the_Order_of_the_Phoenix.jpg', NULL);
insert [BOOK] values ('Harry Potter. Half-Blood Prince', 1, 'Bloomsbury', 572, 2005, 30, 35, 6, 'https://upload.wikimedia.org/wikipedia/en/b/b5/Harry_Potter_and_the_Half-Blood_Prince_cover.png', NULL);
insert [BOOK] values ('Harry Potter. Deathly Hallows', 1, 'Bloomsbury', 635, 2007, 34, 39, 7, 'https://upload.wikimedia.org/wikipedia/en/thumb/a/a9/Harry_Potter_and_the_Deathly_Hallows.jpg/220px-Harry_Potter_and_the_Deathly_Hallows.jpg', NULL);
insert [Book] values ('IT', 2, 'Viking', 1344, 1986, 45, 60, NULL, 'https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781982127794/it-9781982127794_hr.jpg', NULL);
insert [Book] values ('Green Mile', 2, 'Signet Books', 448, 1996, 28, 40, NULL, 'https://d28hgpri8am2if.cloudfront.net/book_images/onix/cvr9781501192265/the-green-mile-9781501192265_hr.jpg', NULL);
insert [Book] values ('The Shining', 2, 'Doubleday', 447, 1977, 45, 60, NULL, 'https://recommerce.com.ua/static/mousebook.reshop.com.ua/catalog/9592/98645475262ce8f33254e3_medium.jpg', NULL);
insert [Book] values ('To Kill a Mockingbird', 3, 'J. B. Lippincott', 281, 1960, 20, 30, 1, 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg/800px-To_Kill_a_Mockingbird_%28first_edition_cover%29.jpg', NULL);

insert [BOOK_DETAILS] values (1, 100, 1), (2, 100, 1), (3, 100, 1), (4, 100, 1), (5, 100, 1), 
(6, 100, 1), (7, 100, 1), (8, 100, 1), (9, 100, 1), (10, 100, 1), (11, 100, 1);

insert BookInfoGenreInfo values (1, 1), (1, 7),
(2, 1), (2, 7),
(3, 1), (3, 7),
(4, 1), (4, 7),
(5, 1), (5, 7),
(6, 1), (6, 7),
(7, 1), (7, 7);

insert BookInfoGenreInfo values (8, 6), (8, 9), (8, 12),
(9, 6), (9, 9), (9, 12),
(10, 6), (10, 9), (10, 12);

insert BookInfoGenreInfo values (11, 10);

insert [user] values ('Admin', 'admin', 'admin123', 1);