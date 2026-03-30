require("dotenv").config();
const express = require("express");
const mysql = require("mysql2");
const cors = require("cors");

const app = express();
app.use(cors());
app.use(express.json());

// ================== KẾT NỐI DATABASE ==================
const db = mysql.createPool({
    host: process.env.DB_HOST || "db",
    user: "root",
    password: "root",
    database: "myapp",
    waitForConnections: true,
    connectionLimit: 10,
    queueLimit: 0
});

// ✅ Test connection đúng cách (KHÔNG dùng connect)
db.getConnection((err, connection) => {
    if (err) {
        console.log("❌ DB connection failed:", err.message);
    } else {
        console.log("✅ Connected to MySQL");
        connection.release(); // trả connection về pool
    }
});

// ================== API ==================

// 👉 GET USERS
app.get("/users", (req, res) => {
    db.query("SELECT * FROM users", (err, result) => {
        if (err) {
            console.log("❌ Query error:", err);
            return res.status(500).json(err);
        }
        res.json(result);
    });
});

// 👉 POST USERS
app.post("/users", (req, res) => {
    const { name } = req.body;

    if (!name) {
        return res.status(400).json({ message: "Name is required" });
    }

    db.query(
        "INSERT INTO users (name) VALUES (?)",
        [name],
        (err, result) => {
            if (err) {
                console.log("❌ Insert error:", err);
                return res.status(500).json(err);
            }
            res.json({
                message: "User added",
                id: result.insertId
            });
        }
    );
});

// 👉 HEALTH CHECK
app.get("/health", (req, res) => {
    res.json({ status: "ok" });
});

// 👉 ABOUT
app.get("/about", (req, res) => {
    res.json({
        app: process.env.APP_NAME,
        name: "NGÔ THÚY DUYÊN",
        mssv: "2251220082",
        class: "22CT2"
    });
});

// ================== START SERVER ==================
const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log("🚀 Server running at port", PORT);
});