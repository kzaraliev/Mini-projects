var Express = require("express");
var MongoClient = require("mongodb").MongoClient;
var cors = require("cors");
const multer = require("multer");

var app = Express();
app.use(cors());

var CONNECTION_STRING =
  "mongodb+srv://kzaraliev:VVLWrvlYuQceMz6q@cluster0.xxsbwww.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

var DATABASE = "tododb";
var database;

app.listen(5038, () => {
  MongoClient.connect(CONNECTION_STRING, (error, client) => {
    database = client.db(DATABASE);
    console.log("Mongo DB Connection Successful");
  });
});

app.get("/api/todoapp/GetNotes", (request, response) => {
  database
    .collection("todocollection")
    .find({})
    .toArray((error, result) => {
      response.send(result);
    });
});

app.post("/api/todoapp/AddNotes", multer().none(), (request, response) => {
  database.collection("todocollection").count({}, function (error, numOfDocs) {
    database.collection("todocollection").insertOne({
      id: (numOfDocs + 1).toString(),
      description: request.body.newTask,
    });

    response.json("Added Successfully");
  });
});

app.delete("/api/todoapp/DeleteNotes", (request, response) => {
  database.collection("todocollection").deleteOne({
    id: request.query.id,
  });

  response.json("Delete Successfully");
});
