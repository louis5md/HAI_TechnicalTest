# HAI_TechnicalTest
This project was built for the purpose of completing HAI technical test

# About this project

Projek ini dibangun dalam bentuk penyelesaian test teknikal pada PT HAI bagian project test (https://github.com/shuujin23/technical-test)

Projek ini membangun API Services, menggunakan C# dengan asp.net core framework. Dengan fungsi-fungsi:

1. Login
2. Logout
3. Create user
4. Update user
5. Update password user
6. Delete user
7. Get list user dengan filter email


## Implementasi

1. Login

route:/user/login

Body:
{
  "email": "string",
  "password": "string"
}

Untuk login, diimplementasikan dengan langkah berupa pemeriksaan email kemudian dilanjut dengan penggunaan Jwt token untuk membandingkan password masukan dengan password yang telah disimpan.
Setiap kali login berhasil, akan dikembalikan data user (kecuali password) dengan tambahan token yang bertahan selama 15 menit.


2. Logout

route:/user/logout

Body:

Header: 
Authorization : "string"   => berisi token

Untuk mengimplementasikan logout, digunakan distributed cache berupa redis untuk menyimpan token2 dari user yang telah logout. Token disimpan untuk periode 15 menit, 
dikarenakan waktu yang lebih dari 15 menit akan otomatis membuat token tidak lagi berlaku (valid). Setelah user memilih logout dengan menggunakan token yang benar, token tersebut akan disimpan dan ditandai sebagai token dari user yang telah logout, sehingga orang yang menggunakan token tersebut untuk aktivitas pada website ini akan dianggap tidak memiliki ijin (unauthorized)

3. Create User

route:/user/register

body:
{
  "name": "string",
  "email": "string",
  "password": "string"
}

Untuk implementasi create user, dilakukan dengan terlebih dahulu memeriksa email sehingga tidak melakukan pendaftaran untuk email yang telah terdaftar di database. Jika email masih baru, maka data disimpan ke database dengan password yang telah di-hash. Setelah create user juga akan diberikan data user (kecuali password) dengan tambahan token

4. update user & update passowrd user

route:/user/update

body:
{
  "name": "string",
  "password": "string",
}
header:
Authorization: "string"   => berisi token

untuk implementasi update user dan update password user dilakukan pada 1 route yang sama. Jika user hanya merubah satu field saja, dan field lainnya bernilai null, dapat ditoleransi dan hanya akan mengupdate data user untuk bagian yang diisi saja. Pemanfaatan token bertujuan untuk menemukan data user yang ingin diubah sekaligus menjadi bentuk ijin pengubahan.


5. Delete user

route:/user/delete/

body:

header:
Authorization: "string"  =>berisi token

Untuk implementasi delete user, dilakukan dengan cara melakukan verifikasi dengan token, lalu mmelakukan penghapusan data user dari pemilik token tersebut

6. Get list user dengan filter email

route:/user/list

body:
{
  "email": "string"
}

Untuk bagian pencarian list user, dilakukan dengan mencari ke database untuk semua user yang memiliki email yang mengandung email masukan. Pada query SQL menggunakan kondisi WHERE email LIKE '%{email}%'. 




# Note penggunaan
projek ini menggunakan postgres sebagai manajemen database dan redis untuk melakukan penyimpanan cache (distributed cache)
