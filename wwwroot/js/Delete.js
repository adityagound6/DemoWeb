function confirmationDelete(id) {
    let text;
    if (confirm("Are You sure to delete!") == true) {
        location.replace("https://localhost:44361/Home/Delete/" + id)
    }
    else {
        alert("Your Data is Safe")
    }
}