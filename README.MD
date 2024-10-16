# Small CMS

  
Welcome to the Small CMS Blog project! This project is a simple content management system (CMS) for blog posts, developed using **.NET Core 8** for the backend and **Vue.js 3** for the frontend. It follows the CQRS pattern, uses MediatR, and features a clean, maintainable architecture.

  

### Features

  

 - User authentication and authorization using JWT.
  
 - Add, edit, delete, and view blog posts for authenticated users.   
 
 - Search functionality to filter blog posts by title or body content.

 - Pagination support for easy navigation through blog posts.

 - Clean architecture with CQRS and MediatR patterns.

 - Full CRUD operations with a rich-text editor (currently disabled).

 - Responsive frontend  UI built with Bootstrap.


### Technologies Used

 - **Backend:** .NET Core 8, C#, Entity Framework Core (EF Core), SQLite, MediatR, ASP.NET Core Authentication.
 
 - **Frontend:** Vue.js 3, Vue Router, Pinia for state management, Bootstrap 5 for styling.
 
 - **Database:** SQLite for simple, lightweight storage.

  

### Setup Instructions
  
Prerequisites:

 - .NET SDK (version 8 or newer)

 - Node.js (version 16 or newer)

 - Vue CLI (globally installed via Node.js)

  

# How to run on local



Open the project in Visual Studio and simply run it.

The project is configured to run on **one** host, so you don’t need to run the front-end application separately. However, the front-end project is completely independent and can be developed without any dependency on the .NET backend project.

Once you successfully build and run the project, use the following address to view the front-end application:

https://localhost:5173/


And for swagger:

http://localhost:5261/swagger/index.html

  
If you encounter any errors, right-click on the solution in Visual Studio, select **Properties**, then go to the **Startup Project** tab. Choose **Multiple startup projects**, and set the action to **Start** for both **SmallCMS.API** and **SmallCMS.UI**.

  

### How to add a blog post:

Log in by clicking the **Login** button.

**username:**   admin@site.com

**password:**  q123456

After logging in, you will be redirected to the dashboard, where you can add or edit posts. At the bottom of the home page, you can view a list of recent posts.

## Notes


 - **Authentication:** Users need to authenticate using the /login endpoint and will receive a JWT token, which must be stored in local storage and used for authenticated requests.
 - **Proxy Setup:** The frontend has a proxy configuration that allows it to communicate seamlessly with the backend while both are running on  different ports.
 **- Exceptions handling :** The backend uses middleware for handling exceptions.
 
 - **CI/CD Pipeline:** The project uses GitHub Actions for continuous integration (CI). The workflow file named **build_and_test.yml** is
   configured to automatically build and test the application with every **push or pull request**.
