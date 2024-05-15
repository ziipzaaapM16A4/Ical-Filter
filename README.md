# Ical-Filter
Ical-Filter is a very small automated programm that pulls an ical file from web, filters events and uploads it via ftp to another server.

Config file layout:
DownloadURL = URL to the Ical File.
UploadURL = The FTP Server to upload the new file. only write "domain/path/to/folder"
Username = FTP Username
Password = FTP Account Password
IdleTime = Time to wait for the next iteration in secconds
filterEvents = Every event "SUMMARY"name which apears to be in this List will be filtered from the modified ical. Seperate like this "vacation,homeoffice,onCall"

Docker Commands:

docker image build ./ -t icalfilter

docker run icalfilter -v ./yourconfigpath/:/config/
