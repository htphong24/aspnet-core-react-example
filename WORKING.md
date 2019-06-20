## Assumptions

The page is an Single Page App uses ASP.NET core and React. It loads data from an CSV files through API and display how many records are loaded. 
We can also click on page number to navigate between pages. The list, number of contacts/pages and pagination will get refreshed as user types in search bar. Here is an screen-shot.

![App Screenshot](screenshot.gif)

## Additional tools or notes

+ [React-Pagination](https://www.npmjs.com/package/react-pagination)
+ [Datatables](https://mdbootstrap.com/docs/jquery/tables/scroll)
+ Other references: [Build Custom Pagination with React](https://scotch.io/tutorials/build-custom-pagination-with-react), [Setting A React roject From Scratch Using Babel And Webpack](https://blog.bitsrc.io/etting-a-react-project-from-scratch-using-babel-and-webpack-5f26a525535d)

## Design decisions

### Front-end:

+ **components**: contains Pagination and ContactRow components for the React App. Separate ContactRow into a component is not necessary and an be simplified but this can be useful if we need to change layout of each row to something else (e.g. a card)
+ App.js: main component of the app
+ constant.js: contains constants

### Back-end: 
+ **Common** contains interfaces
+ **Controllers**: contains actions along with their API, which repositories will be used is also injected here
+ **Models**: contains models
+ **Repositories**: contains repositories which implement interfaces in Common folder
+ **Util**: contains file loader utility to load data from CSV file


## Additional nice to haves or features that you might suggest but do not have time to complete.

+ I am currently loading all data from CSV file to front-end, however if the file grows (e.g. millions of rows) then it should be paginated ight from back-end to only load necessary data.
+ The page can add a form to add new contact to the list and make each column sortable.
+ Editable rows is also a nice feature to have but it propably takes much time to do.

## Time summary:

+ Preparation: 2 hour
+ Coding: 8 hours (including tests)
+ Styling: 30 minutes
+ Building and testing: 1 hour

Grand total: 11.5 hours

Note that research time is not counted here, this took me almost 2 days to understand Javascript Services (its tutorial is outdated) and do a ew examples of React.js

