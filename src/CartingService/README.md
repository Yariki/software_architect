Task 1 (week 1):

Create BLL (business logic layer) and DAL (data-access layer) for Carting Service. Any implementation of the layered architecture can be used. Layers should be logically separated (via separate folders/namespaces).

Functional Requirements:

Single entity – Cart
Cart has a unique id which is maintained (generated) on the client-side.
The following actions should be supported:
Get list of items of the cart object.
Add item to cart.
Remove item from the cart.
Each item contains the following info:
Id – required, id of the item in external system (see Task 2), int.
Name – required, plain text.
Image – optional, URL and alt text.
Price – required, money.
Quantity – quantity of items in the cart.

Non-functional Requirements (NFR):

Testability
Extensibility
Constraints:

NoSQL database for persistence layer (for example - <https://www.litedb.org/>).
Layers should be logically separated.
