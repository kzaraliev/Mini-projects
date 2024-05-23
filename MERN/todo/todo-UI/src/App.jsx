import { useState, useEffect } from "react";
import "./App.css";

const API_URL = "http://localhost:5038";

function App() {
  const [tasks, setTasks] = useState([]);
  const [newTask, setNewTask] = useState("");

  useEffect(() => {
    //Stop fetch request when page is being closed
    //const abortController = new AbortController();

    fetch(`${API_URL}/api/todoapp/GetNotes`)
      .then((res) => res.json())
      .then((data) => {
        setTasks(data);
      });

    // return () => {
    //   abortController.abort();
    // };
  }, []);

  const handleInputChange = (event) => {
    setNewTask(event.target.value);
  };

  console.log(newTask);

  async function addTaskHandler() {
    const data = new FormData();
    data.append("newTask", newTask);

    fetch(`${API_URL}/api/todoapp/AddNotes`, {
      method: "POST",
      body: data,
    })
      .then((res) => res.json())
      .then((data) => {
        const lastId =
          tasks.length > 0 ? Math.max(...tasks.map((task) => task.id)) : 0;
        const newTaskObject = {
          id: lastId + 1,
          description: newTask,
        };

        setTasks((prevTasks) => [...prevTasks, newTaskObject]);
        setNewTask(""); // Clear the input field
      });
  }

  async function deleteTaskHandler(id) {
    var newTask = document.getElementById("newTask").value;

    const data = new FormData();
    data.append("newTask", newTask);

    fetch(`${API_URL}/api/todoapp/DeleteNotes?id=${id}`, {
      method: "DELETE",
      body: data,
    })
      .then((res) => res.json())
      .then((data) => {
        setTasks((prevTasks) => prevTasks.filter((task) => task.id !== id));
      });
  }

  return (
    <>
      <h2>Todo App</h2>
      <input
        id="newTask"
        type="text"
        value={newTask}
        onChange={handleInputChange}
        placeholder="Enter new task"
      />
      <button onClick={() => addTaskHandler()}>Add Task</button>

      {tasks && tasks.length > 0 ? (
        tasks.map((task) => (
          <div key={task.id}>
            <p key={task.id}>{task.description}</p>
            <button onClick={() => deleteTaskHandler(task.id)}>
              Delete Task
            </button>
          </div>
        ))
      ) : (
        <p>No tasks available</p>
      )}
    </>
  );
}

export default App;
