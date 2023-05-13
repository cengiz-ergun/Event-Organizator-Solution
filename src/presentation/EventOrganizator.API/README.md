<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#response-types">Response Types</a>
      <ul>
        <li><a href="#succesful-response">Succesful Response</a></li>
        <li>
            <a href="#failed-response">Failed Response</a>
               <ul>
                    <li><a href="#semantic-errors">Semantic Errors</a></li>
                    <li><a href="#lexical-errors">Lexical Errors</a></li>
                    <li><a href="#not-found">Not Found</a></li>
               </ul>
        </li>
      </ul>
    </li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## Response Types
You can handle http response content according to http response stasuses.

### <a>Succesful Response</a>
- If response has content, http status will be 200 OK. Body will be like:
````
{
  "payload": [
    {
      "id": "949f3b5c-3378-497e-a388-656b287cad5d",
      "userName": "FakeUserName",
      "firstName": "FakeFirstName",
      "lastName": "FakeLastName",
      "email": "fake@mail.com"
    }
  ],
  "count": 1
}
````

- If response has no content, http status will be 204 No Content.
<hr>

### Failed Response

#### Semantic Errors
- If there are semantic errors, http status will be 422 Unprocessable Content. Body will be like:
````
{
  "errors": [
    "Passwords must be at least 10 characters.",
    "Passwords must have at least one digit ('0'-'9')."
  ]
}
````

#### Lexical Errors
- If there are lexical errors, http status will be 400 Bad Request. Body will be like:
````
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-d1b9f5685fe426d287abaa50c1d15cea-21e2eb3c7d8ef541-00",
  "errors": {
    "FirstName": [
      "FirstName is required!"
    ]
  }
}
````

#### Not Found
- If resource not found, http status will be 404 Not Found. Body will be like:
````
{
  "message": "There is not a registered user with e-mail fakemail@mail.com."
}
````

#### Unauthorized
- If password is wrong for login attempt, http status will be 401 Unauthorized. Body will be like:
````
{
  "message": "Password is not valid."
}
````
