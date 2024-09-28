async function fetchUsers() {
    try {
        const response = await fetch('https://localhost:7231/api/Relations/dsd'); // Replace with your actual API URL
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const users = await response.json();
		console.log(users);
        displayUsers(users);
    } catch (error) {
        console.error('Error fetching user data:', error.message);
    }
}

function displayUsers(users) {
    const userContainer = document.getElementById('userContainer');
    userContainer.innerHTML = ''; // Clear the container

    users.forEach(user => {
        const userCard = document.createElement('div');
        userCard.className = 'user-card';
        userCard.innerHTML = `<strong>Name:</strong>${user.id} ${user.name} <br> <strong>Address:</strong> ${user.address}`;
        userContainer.appendChild(userCard);
    });
}

// Call the function to fetch users when the page loads
fetchUsers();