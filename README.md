Write a WPF project using devexpress ([https://www.devexpress.com/](https://www.devexpress.com/)) or any other 3rd party library to display a table with fake data.

It should be possible to select multiple lines.
By right-clicking on a table row, a context menu should be displayed.
The context menu should contain two item menus to export
- in CSV
- in TXT

By clicking one of the item menu, the selected lines should be exported to the appropriate format CSV or TXT

What will be paid attention to: 
1.  Code quality 
2.  Usage programming patterns, etc. 

Minimum requirements:
1.  Using MVVM and programming patterns, etc. 
2.  Usage of Dependency Injection.


### Implementation

This project was created by using DevExpress v22.2 as was mentioned in the task.

#### What was done.

1. As data source was downloaded and installed the Northwind Database and installed as ADO.NET EntityFramework module (NorthwindData project) as in documentation for DevExpress. Only three tables wass used (Orders, Employees and Shippers).
2. MVVM aschitecure was used. Adedd MvvmDialogsLibrary as separated library class to manage custom mvvm dialogs.
3. For serialisation logic for CSV and TXT fiiles was created separate class library (DevTrustDemoSerializationLibrary).
4. As IoC contaiterization was used Castle Windsor. Containers bootstrapping located in one entrypoint (class BootstrapContainer).
5. Creation CSV and TXT files was implemented in contect menu of Orders table. 


#### What was not done.

1. Utit tests not created. This item is not explicitly stated in the assignment. Therefore, with a very small amount of logic, this item is omitted for the time being.
2. Models in the main project wa not created beacuse in library NorthwindData that objects already declared. In full functional application, where used DAO layer, this Model must be implemented.
3. DAO (CRUD) layer was not created. 


#### Problems. 

Running program in Visual Studio in debug mode may cause the program to stop working when a dialog box for selecting a file to save is displayed. This issue has not been completely resolved. However, in other modes everything works fine. To eliminate this problem you can try to set 'Enable native code debugging' in the main project (DevTrustdemoUI) settings.

#### Installation

1. Install DevExpress (minimum version 22.2).
2. Clone this repository to your local machine.
3. Set DevtTrustDemoUI project as startup project.

#### My comments.

This is first time of using by me the DevExpress framework. In my last big progect I used Sincfusion. Implementation of MVVM pattern is on very basic level and may vary in different solutions and companies. Just discover the code and fill free in comments. All your comments will be carefully considered and taken into account in future work.

