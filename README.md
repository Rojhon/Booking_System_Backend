# Booking_System_Backend
Booking System Api
Don’t forget to include - https://localhost:44310/

**Request System**
api/request/create-request
api/request/get-request/{trackingId}
api/request/delete-request/{trackingId}
api/request/update-request
api/request/get-all-offices
api/request/get-all-services
api/request/get-all-requests

**Some Example of Http requests using Fetch in Js for Request
System.**

**Create Request**
var url = “https://localhost:44310/api/request/create-request”
var values = {
OfficeId: office.value,
ServiceId: service.value,
UserNote: userNote.value,
FileData: reader.result.split(",")[1]
}
const response = await fetch(url, {
method: "POST",
headers: { 'Content-Type': 'application/json' },
body: JSON.stringify(values)
});

Get Request by Tracking Id
var url =
‘https://localhost:44310/api/request/get-request/${trackingId}’
const response = await fetch(url);
var data = await response.json();
console.log(data)
Get All Request
var url = “https://localhost:44310/api/request/get-all-requests”
const response = await fetch(url);
var data = await response.json();
console.log(data)
You c
