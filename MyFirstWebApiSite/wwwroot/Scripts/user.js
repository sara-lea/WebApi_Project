
const register = async () => {
    const strength = checkPassword();
   /* if (strength > 3) {*/


        const userData = {
            firstname: document.getElementById("firstName").value,
            lastname: document.getElementById("lastName").value,
            email: document.getElementById("userName").value,
            password: document.getElementById("password").value
    }



        const responsePost = await fetch('api/user/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        });
        const dataPost = await responsePost.json();

        if (responsePost.ok) {
            window.location.href = "login.html";
         }
        else {
            alert("אופס, אחד או יותר מן הנתונים שגוי...")
        } 

    //}
    //else {
    //    alert("password is not strong enough");
    //}
}

const login = async () => {
    const userData = {
        email: document.getElementById("userName").value,
        password: document.getElementById("password").value
    }
 
    const responsePost = await fetch('api/user/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
    const dataPost = await responsePost.json();
   
    if (responsePost.ok) {
        console.log(dataPost)
        sessionStorage.setItem("user", JSON.stringify(dataPost));
        window.location.href = "home.html";
    }
    else {
        alert("שם משתמש או סיסמא אינם תקינים")
    }
}

const update = async () => {
    
    const userData = {
        FirstName: document.getElementById("firstName").value,
        LastName: document.getElementById("lastName").value,
       Email: document.getElementById("userName").value,
        Password: document.getElementById("password").value,
        id:JSON.parse(sessionStorage.getItem("user")).userId
}
   
    console.log(userData)
 
    const responsePost = await fetch(`api/user/`+userData.id, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
    //const res = await responsePost.json();
  
    if (responsePost.ok) {
        alert("הפרטים עודכנו בהצלחה");
        window.location.href = "login.html";
    }
    else {
        alert("אחד או יותר מהפרטים אינם תקינים...")
        
    }
}

const checkPassword = async () => {
  
    password = document.getElementById("password").value;
    const responsePost = await fetch('api/user/checkPassword', {
        method: 'POST',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(password)
    })
    const dataPost = await responsePost.json();
    var color = ''
    document.getElementById("progress").setAttribute("value", dataPost)
    if (dataPost <= 1)
        color = '#ff0000';
    else if (dataPost <= 3)
        color = 'blue'
    else
        color = '#4cff00'
    document.getElementById("progress").style.setProperty("accent-color", color)
    document.getElementById("strentgh").innerHTML = "strength: " + dataPost;
    return parseInt(dataPost);
}