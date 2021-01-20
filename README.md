# MockApi

Really simple Mock API server.

## Usage

in Postman (or equivalent):
Where (for example):
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
Currently, 'ResponseCache' saves the reponses in memory - not very persistent.
Uses 'System.text.Json' which seems to like all JSON properties to be quoted.