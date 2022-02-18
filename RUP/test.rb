# p 'https://www.themoviedb.org/t/p/w600_and_h900_bestv2' + '/xhU6hPDnk4s8iWWKoq4oEmNI600.jpg'
# p 32.512412412312.round(2

  # insert_string = "INSERT INTO [Movies] (Title, Description, Release_date, Length, Poster_path, Stills_path, Rating) VALUES (?, ?, ?, ?, ?, ?, ?)"
  # client.execute(insert_string, [title, description, release_date, length, poster_path,'Stills_path', rating]).do)

a = "abababa////b/`a//dsadsa'ba\""
a.gsub!(/[\"]/,'')
p a