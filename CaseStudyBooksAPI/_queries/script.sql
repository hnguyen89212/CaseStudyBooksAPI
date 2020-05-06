INSERT INTO Brands(Name)
VALUES
('Packt Publishing'),
('Harper Colins'),
('McGraw Hill Education'),
('Prentice Hall'),
('Pearson Education'),
('Cencage'),
('Wiley'),
('Penguine Random House');

INSERT INTO Products(ProductName, BrandId, CostPrice, MSRP, QtyOnHand, QtyOnBackOrder, Description, ReleasedYear)
VALUES
('Working Effectively with Legacy Code', 2, 40.24, 39.86, 100, 2, 'Is your code easy to change? Can you get nearly instantaneous feedback when you do change it? Do you understand it? If the answer to any of these questions is no, you have legacy code, and it is draining time and money away from your development efforts.', 2004),
('Programming Pearls', 1, 32.00, 35.67, 24, 3, 'Fourteen years after it was first issued, C++ expert Jon Bentley reinvents a true classic with the second edition of his Programming Pearls. Completely revised and brought up to date with all new code examples in C and C++, this book remains an exceptional tutorial for learning to think like a programmer.', 1999),
('The Mythical Man-Month', 3, 23.97, 20.10, 56, 2, 'The classic book on the human elements of software engineering. Software tools and development environments may have changed in the 21 years since the first edition of this book, but the peculiarly nonlinear economies of scale in collaborative work and the nature of individuals and groups has not changed an epsilon. If you write code or depend upon those who do, get this book as soon as possible -- from Amazon.com Books, your library, or anyone else. You (and/or your colleagues) will be forever grateful. Very Highest Recommendation.', 1995),
('Dont Make Me Think', 4, 31.34, 32.25, 28, 2, 'I believed that with a little bit of instruction most people could do a lot of what I do themselves, since much of it just seems like common sense once you hear it explained.', 2014),
('The Pragmatic Programmer', 5, 42.35, 40.00, 89, 1, 'Programmers are craftspeople trained to use a certain set of tools (editors, object managers, version trackers) to generate a certain kind of product (programs) that will operate in some environment (operating systems on hardware assemblies). Like any other craft, computer programming has spawned a body of wisdom, most of which isnt taught at universities or in certification classes. Most programmers arrive at the so-called tricks of the trade over time, through independent experimentation. In The Pragmatic Programmer, Andrew Hunt and David Thomas codify many of the truths theyve discovered during their respective careers as designers of software and writers of code.', 1999);