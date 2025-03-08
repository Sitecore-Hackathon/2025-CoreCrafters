document.addEventListener("DOMContentLoaded", function () {
    var chatContainer = document.getElementById("chat-container");

    window.toggleChat = function () {
        chatContainer.style.display = (chatContainer.style.display === "none" || chatContainer.style.display === "") ? "block" : "none";
    };

    document.getElementById("chat-input").addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault(); // Prevent default newline behavior
            document.getElementById("chat-form").submit(); // Submit form
        }
    });
});
