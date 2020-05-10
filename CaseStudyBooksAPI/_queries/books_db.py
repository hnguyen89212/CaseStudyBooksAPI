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

storageImages = [
    # "O Reilly Media"
    {
        "Modern PHP: New Features and Good Practices": "mordern_php.jpg"
    },
    {
        "Learning PHP, MySQL & JavaScript: With jQuery, CSS & HTML5": "learning_php_mysql_js.jpg"
    },
    {
        "CSS: The Definitive Guide": "css_the_definitive_guide.jpg"
    },
    {
        "JavaScript: The Good Parts": "js_the_good_part.jpg"
    },
    {
        "Learning JavaScript Design Patterns": "learning_js_design_patterns.jpg"
    },
    # "Apress"
    {
        "PHP Objects, Patterns, and Practice": "php_objects_patterns_and_practice.jpg"
    },
    {
        "PHP 7 Solutions: Dynamic Web Design Made Easy": "php_7_solutions.jpg"
    },
    {
        "Practical PHP 7, MySQL 8, and MariaDB Website Databases": "practical_php_7_mysql_8.jpg"
    },
    {
         "The Full Stack Developer: Your Essential Guide": "the_full_stack_developer.jpg"
    },
    {
        "SQL Server 2019 Revealed: Including Big Data Clusters and Machine Learning": "sql_server_2019_revealed.jpg"
    },
    # "Wrox"
    {
        "Professional WordPress: Design and Development": "professional_wordpress.jpg"
    },
    {
        "Professional C# 7 and .NET Core 2.0": "professional_cs7.jpg"
    },
    {
        "Beginning Linux Programming": "beginning_linux_programming.jpg"
    },
    {
        "Beginning Android Programming with Android Studio": "beginning_android_programming.jpg"
    },
    {
        "Professional Git": "pro_git.jpg"
    },
    # "For Dummies"
    {
        "Critical Thinking Skills For Dummies": "ciritical_thinking.jpg"
    },
    {
        "Economics For Dummies": "economics.jpg"
    },
    {
        "Reading Financial Reports For Dummies": "reading_fin_report.jpg"
    },
    {
        "Financial Modeling in Excel For Dummies": "financial_modeling.jpg"
    },
    {
        "Statistical Analysis with Excel For Dummies": "stat_analysis.jpg"
    },
    # "OSTraining"
    {
        "WooCommerce Explained": "woocommerce_ex.jpg"
    },
    {
        "Local Web Development With DDEV Explained": "wordpress_plugin_ex.jpg"
    },
    {
        "WordPress Plugins Explained": "local_web_dev_with_ddev.jpg"
    },
    {
        "Drupal 8 Explained": "drupal_8_ex.jpg"
    },
    {
        "Multilingual Joomla Explained": "multilingual_joomla_ex.jpg"
    }
]

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

updateScript = open("./update-script.sql", "w+")

# UPDATE Products SET GraphicName = 'mouse' WHERE ProductName = 'Beginning Android Programming with Android Studio'

for each in storageImages:
    for key in each:
        bookName = key
        graphicName = each[bookName]
        updateScript.writelines("UPDATE Products SET GraphicName = \'" +
                                graphicName + "\' WHERE ProductName = \'" + bookName + "\';")
        updateScript.writelines("\n")
