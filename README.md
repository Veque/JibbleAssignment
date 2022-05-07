# Assignment for Jibble
The assignment was:
Create a C# Console application utilizing public OData API from: https://www.odata.org/odata-services/ (use v4). The app should :-
  1) List people
  2) Allow searching/filtering people
  3) Show details on a specific Person

Please submit your project to your own repository (Github, BitBucket, Google Drive, etc.). This test will demonstrate your ability to work with libraries and APIs, implementation will be discussed in the technical interview. Bonus points for:
  1) Modifying data
  2) Using best practices
  3) Tests
  
## My notes.
I decided not to use the specific OData client to consume the service. Instead, I consumed it as a generic REST service because I figured it is a more generic solution showing the ability to work with HTTP services, serializing and deserializing objects mself without "auto-generation"

To save some time I didn't include all the possible fields of the returned objects, since my objective was to show technical skill, not to make a very useful application. So I thought that adding all those fields that I omitted in the output won't influence y image as a programmer.

Also, to save some time I didn't make tests. Usually, I make unit tests to test logic, but there are almost no logic. Just requesting data and putting it in console, so not much to test there. Although, I could write some "Integration tests", checking the correctness of the integration with Trippin service, but
  1) Sadly, I didn't have much time left
  2) Usually, these kinds of integration tests work better when done outside the applicatio by some other framework testing multiple services working well together.
So sorry for the absence of tests)

When using "instance key" trying to make my modifications of data in the Trippin service visible I encountered an error "Value cannot be null. Parameter name: edmEntityObject" with code 500. I couldn't figure out what it was and how to fix it. From the code 500 I assumed tat the problem was not in my code but in their code, since 500 essentially means "it's server's error". I read the documentation carefully, did everything they wrote there to do to make modifications visible, but I had no clue, how to fix the error or why it happened.

Without the "instance key" it works fine, but you can't see the modified data.

Thank you!
