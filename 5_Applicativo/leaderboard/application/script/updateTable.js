const displayUser = (users) => {
    const template = document.querySelector("#courseTemplate");
    const container = document.querySelector("#userContainer");

    container.innerHTML = '';
    console.log("user");
    users.forEach(user => {
        const clone = template.content.cloneNode(true);
        console.log(URL);
        const link = URL + "friendController/friendRequest/" + user.id;
        clone.querySelector('.addFriendBtn').href = link;
        clone.querySelector('.username').textContent = user.username;
        clone.querySelector('.score').textContent = user.score;

        container.appendChild(clone);
    });
};