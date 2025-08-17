from flask import Flask, render_template, request, redirect, url_for, send_from_directory, session
import os
from admin_credentials import ADMIN_USERNAME, ADMIN_PASSWORD

app = Flask(__name__)
app.secret_key = "supersecretkey"  # Required for session

BOOK_FOLDER = os.path.join(os.getcwd(), "books")
books = [
    {"title": "Sample Book", "file": "sample.pdf", "cover": "sample_cover.jpg"}
]

# ----------- USER SIDE -----------

@app.route("/")
def index():
    return render_template("index.html", books=books)

@app.route("/download/<filename>")
def download(filename):
    return send_from_directory(BOOK_FOLDER, filename, as_attachment=True)

# ----------- ADMIN LOGIN -----------

@app.route("/admin/login", methods=["GET", "POST"])
def admin_login():
    if request.method == "POST":
        username = request.form["username"]
        password = request.form["password"]
        if username == ADMIN_USERNAME and password == ADMIN_PASSWORD:
            session["admin_logged_in"] = True
            return redirect(url_for("admin_dashboard"))
        else:
            return "Invalid credentials!"
    return render_template("login.html")

# ----------- ADMIN DASHBOARD -----------

@app.route("/admin")
def admin_dashboard():
    if not session.get("admin_logged_in"):
        return redirect(url_for("admin_login"))
    return render_template("admin.html", books=books)

@app.route("/admin/add", methods=["POST"])
def add_book():
    if not session.get("admin_logged_in"):
        return redirect(url_for("admin_login"))

    title = request.form["title"]
    file = request.files["file"]
    cover = request.files["cover"]

    file.save(os.path.join(BOOK_FOLDER, file.filename))
    cover.save(os.path.join("static/images", cover.filename))

    books.append({"title": title, "file": file.filename, "cover": cover.filename})
    return redirect(url_for("admin_dashboard"))

@app.route("/admin/delete/<int:index>")
def delete_book(index):
    if not session.get("admin_logged_in"):
        return redirect(url_for("admin_login"))

    books.pop(index)
    return redirect(url_for("admin_dashboard"))

@app.route("/admin/logout")
def admin_logout():
    session.pop("admin_logged_in", None)
    return redirect(url_for("index"))

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)
