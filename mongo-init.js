db.createUser(
    {
        user: "usrapi",
        pwd: "UserApi123$",
        roles: [
            {
                role: "readWrite",
                db: "ClientesStoreDb"
            }
        ]
    }
);