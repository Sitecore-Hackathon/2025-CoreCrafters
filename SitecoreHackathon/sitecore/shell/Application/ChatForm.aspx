<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatForm.aspx.cs" Inherits="YourNamespace.ChatForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>ChatBot</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }
        #chatContainer {
            width: 100%;
            height: 100%;
            display: flex;
            flex-direction: column;
            border: 1px solid #ccc;
            background: #f9f9f9;
        }
        #chatBox {
            flex-grow: 1;
            overflow-y: auto;
            padding: 10px;
            border-bottom: 1px solid #ccc;
        }
        #chatInput {
            display: flex;
            padding: 10px;
            background: #fff;
        }
        #chatInput input {
            flex-grow: 1;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        #chatInput button {
            margin-left: 10px;
            padding: 8px 12px;
            background: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <div id="chatContainer">
        <div id="chatBox">
            <!-- Chat messages will be displayed here -->
        </div>
        <div id="chatInput">
            <input type="text" id="message" placeholder="Type a message..." />
            <button onclick="sendMessage()">Send</button>
        </div>
    </div>

    <script>
        function sendMessage() {
            var input = document.getElementById("message");
            var chatBox = document.getElementById("chatBox");

            if (input.value.trim() !== "") {
                var messageDiv = document.createElement("div");
                messageDiv.textContent = input.value;
                messageDiv.style.background = "#007bff";
                messageDiv.style.color = "white";
                messageDiv.style.padding = "8px";
                messageDiv.style.margin = "5px 0";
                messageDiv.style.borderRadius = "4px";
                messageDiv.style.textAlign = "right";

                chatBox.appendChild(messageDiv);
                input.value = "";
                chatBox.scrollTop = chatBox.scrollHeight;
            }
        }
    </script>
</body>
</html>
