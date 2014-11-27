<% @ Language=VBScript %>

<%
Option Explicit
dim strConnectionString, con
set conn=Server.CreateObject("ADODB.Connection")
conn.Provider="Microsoft.Jet.OLEDB.4.0"
on error resume next
conn.Open "e:/feedback_database.mdb"
if error<>0 then
reponse.write(err.description)
else
respose.write("ff")
end if
ReSPonse.Write("fff")
dim fname,lname,gender
dim occ,email,tele,address,city,pincode
dim state,country,comments
fname=Request.Form("fname")
lname=Request.Form("lname")
gender=Request.Form("gender")
occ=Request.Form("occupation")
email=Request.Form("email")
tele=Request.Form ("telephone")
address=Request.Form ("address")
city=Request.Form ("city")
pincode=Request.Form ("pincode")
state=Request.Form ("state")
country=Request.Form ("country")
comments=Request.Form("comments")

%>

