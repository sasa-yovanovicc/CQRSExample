# Description

This application uses the MVC architecture combined with CQRS (Command Query Responsibility Segregation) design pattern. CQRS separates the operations of reading data (Query) from modifying data (Command), suggesting that distinct models should be used for each type of operation.

### Benefits of CQRS:

- **Scalability:** Allows for different performance and optimizations for reading and writing.
- **Clear Separation of Concerns:** Separate logic for reading and writing facilitates easier implementation and maintenance.
- **Flexibility:** Easier to extend and modify parts of the system without affecting others.

The application adheres to SOLID principles and follows a structured project layout.

### Note:

Access parameters such as connection strings and API keys are defined in `appsettings.json` for development purposes. However, for production environments, it is essential to secure these parameters to meet security standards. While this application does not currently implement secure management for these parameters, it is recommended to use managed secrets services such as Azure Key Vault or AWS Secrets Manager in a production setup. These services provide secure storage and management of sensitive information, helping to protect against unauthorized access and ensure compliance with security best practices.

### Project Structure:

- **Commands:** Contains commands for data modification.
- **Controllers:** Controllers that handle HTTP requests.
- **Data:** Manages data access and database interactions.
  - **Entities:** Defines the models that correspond to the physical tables in the database. Each entity represents a specific table structure and its columns.
  - **Repositories:** Provides methods for accessing and manipulating data in the database, enabling operations such as create, read, update, and delete (CRUD) on entities.
- **Handlers:** Contains handlers for commands and queries.
- **Interfaces:** Defines interfaces for service layers.
- **Mapping:** Object mapping configurations.
- **Migrations:** Manages database migrations.
- **Models:** Defines data models.
- **Program.cs:** Application entry point.
- **Queries:** Contains queries for data retrieval.
- **Services:** Contains service layers and business logic.
- **Validators:** Validators for data integrity.
- **Views:** Contains views and user interface.
- **wwwroot:** Static files like CSS, JavaScript, and images.
- **appsettings.json:** Main application configuration.
- **Properties:** Project properties.

### Testing:

The application includes comprehensive testing coverage with both unit and integration tests. A total of 44 tests cover critical elements of the application. The tests focus on:

- **Controllers:** Ensuring the correct handling of HTTP requests and responses.
- **Mappers:** Verifying accurate mapping between different data models.
- **Validators:** Confirming the integrity and correctness of data validation logic.
- **Repositories:** Testing data access and interactions with the database.
- **Services:** Validating the business logic and service layer functionalities.


### Requirements:

The task is to create an ASP.NET MVC application to track employee working hours.

The initial screen should display a table with the following data:
- First Name
- Last Name
- Job Title
- Date of Joining
- Total Worked Hours

Example:

| First Name | Last Name  | Job Title    | Date of Joining | Total Worked Hours |
|------------|-------------|--------------|-----------------|--------------------|
| Ján        | Nový        | Technician   | 1.1.2024        | 15                 |
| Ondrej     | Lehotský    | Developer    | 2.1.2024        | 10                 |

The application must allow the recording of employee arrival and departure times (both the date and time of arrival and departure must be recorded).

The application must support adding multiple intervals for employee arrivals and departures.

Example:

| Arrival          | Departure        |
|------------------|------------------|
| Interval 1:      |                  |
| 1.1.2024 9:00    | 1.1.2024 17:00   |
| Interval 2:      |                  |
| 2.1.2024 9:00    | 2.1.2024 17:00   |

Upon recording the arrival and departure, the total worked time on the initial screen should be updated.

If the intervals overlap, the overlapping time should only be counted once.

Example:

| Arrival          | Departure        |
|------------------|------------------|
| Interval 1:      |                  |
| 1.1.2024 9:00    | 1.1.2024 13:00   |
| Interval 2:      |                  |
| 1.1.2024 11:00   | 1.1.2024 17:00   |

Total worked time = 8 hours.

