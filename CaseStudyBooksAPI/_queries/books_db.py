'''
  5 publishers, each has 5 books.  
'''

import random
import string


def randomString(stringLength=8):
    letters = string.ascii_lowercase
    return ''.join(random.choice(letters) for i in range(stringLength))


storage = {
    "O Reilly Media": [
        "Modern PHP: New Features and Good Practices",
        "Learning PHP, MySQL & JavaScript: With jQuery, CSS & HTML5",
        "CSS: The Definitive Guide",
        "JavaScript: The Good Parts",
        "Learning JavaScript Design Patterns"
    ],
    "Apress": [
        "PHP Objects, Patterns, and Practice",
        "PHP 7 Solutions: Dynamic Web Design Made Easy",
        "Practical PHP 7, MySQL 8, and MariaDB Website Databases",
        "The Full Stack Developer: Your Essential Guide",
        "SQL Server 2019 Revealed: Including Big Data Clusters and Machine Learning"
    ],
    "Wrox": [
        "Professional WordPress: Design and Development",
        "Professional C# 7 and .NET Core 2.0",
        "Beginning Linux Programming",
        "Beginning Android Programming with Android Studio",
        "Professional Git"
    ],
    "For Dummies": [
        "Critical Thinking Skills For Dummies",
        "Economics For Dummies",
        "Reading Financial Reports For Dummies",
        "Financial Modeling in Excel For Dummies",
        "Statistical Analysis with Excel For Dummies"
    ],
    "OSTraining": [
        "WooCommerce Explained",
        "Local Web Development With DDEV Explained",
        "WordPress Plugins Explained",
        "Drupal 8 Explained",
        "Multilingual Joomla Explained"
    ]
}

script = open("./script.sql", "w+")

brandId = 1

script.writelines('INSERT INTO Brand(Name)\nVALUES\n')
for key in storage:
    script.writelines("(\'" + key + "\'),\n")

script.writelines("\n")

script.writelines(
    'INSERT INTO Products(ProductName, BrandId, CostPrice, MSRP, QtyOnHand, QtyOnBackOrder, Description, ReleasedYear)\nVALUES\n')

for key in storage:
    for book in storage[key]:
        costPrice = round(random.uniform(20.00, 150.00), 2)
        msrp = str(round(random.uniform(costPrice - 10.00, costPrice + 5.00), 2))
        costPrice = str(costPrice)
        qtyOnHand = str(random.randint(50, 100))
        qtyOnBackOrder = str(random.randint(2, 10))
        # desc = "description"
        desc = book + ": " + randomString(100)
        releaseYear = str(random.randint(1990, 2019))
        script.writelines("(\'" + book + "\', " + str(brandId) + ", " + costPrice + ", " + msrp +
                          ", " + qtyOnHand + ", " + qtyOnBackOrder + ", \'" + desc + "\', " + releaseYear + "),")
        script.writelines("\n")
    brandId += 1
