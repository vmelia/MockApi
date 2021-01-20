# MockApi

Really simple Mock API server.

## Usage
We PUT to an endpoint using a specific JSON object: "StatusCode", "HttpMethod", "ResponseBody".</br>
Then if we 'GET' or 'POST' the same endpoint, the 'StatusCode' and ResponseBody' will be returned.</br>
A session Id can be used to facilitate parallel use (optionally a Guid string).</br>

## Example
In Postman (or equivalent):</br>
Where (for example):</br>
  Server = https://localhost:5001/
  SessionId = 000000-0000-0000-0000-000000000001/

PUT {{Server}}{{SessionId}}dummy_endpoint
Body (raw) =
{
    "StatusCode": 201, "HttpMethod": "GET",
    "ResponseBody": {
        "get_stuff" :{
            "key": "xxxx",
            "value": 99
        }
    }
}

Then, if we call: {{Server}}{{SessionId}}dummy_endpoint

We should get the StatusCode/ResponseBody sent back.
201-Created
{
  "get_stuff": {
    "key": "xxxx",
    "value": 99
  }
}

## Notes
Currently, 'ResponseCache' saves the reponses in memory - not very persistent.</br>
Uses 'System.text.Json' which seems to like all JSON properties to be quoted.</br>
There are no unit tests! All the logic is in the middleware so I'll have to resarch testing that.</br>
