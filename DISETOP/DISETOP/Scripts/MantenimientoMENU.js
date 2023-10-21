document.addEventListener("DOMContentLoaded", () => {
    var currentEl = null
    // Get the modal
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal
    var rows = document.querySelectorAll(".row");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    rows.forEach(row => {
        row.onclick = function (el) {
            console.log(el.currentTarget)
            modal.style.display = "block";
            currentEl = el.currentTarget
            currentEl.style.pointerEvents = "none"; //auto
        }
    })

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
        currentEl.style.pointerEvents = "auto"; //auto
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            currentEl.style.pointerEvents = "auto"; //auto
        }
    }
});