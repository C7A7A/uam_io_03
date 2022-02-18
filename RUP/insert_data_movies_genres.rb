require 'tiny_tds'
require 'themoviedb'

client = TinyTds::Client.new username: 'xxx', password: 'xxxx',  
host: 'xxx', port: xxx,  
database: 'xxx' 

client.execute("DELETE FROM [Genres]")
client.execute("DELETE FROM [Movies]")
client.execute("DELETE FROM [Movies_Genres]")


Tmdb::Api.key("xxxxxx")
Tmdb::Api.language("pl")

genres = Tmdb::Genre.list

puts 'INSERT GENRES'

records_genres = 0

genres['genres'].each do |genre|
  insert_string = "INSERT INTO [Genres] (Name) VALUES ('#{genre['name']}')"
  client.execute(insert_string).do
  records_genres = records_genres + 1
end

puts "#{records_genres} Inserted into Genres"

puts 'GET GENRES ID'
genres = Array.new
result = client.execute("SELECT * FROM Genres")
result.each_with_index do |genre, key|
  genres[key] = genre
end

puts 'INSERT MOVIES'

records_movies = 0
records_movies_genres = 0

(0..2000).each_with_index do |movie, key|
  movie = Tmdb::Movie.detail(key)
  if movie['status_code'].nil?

    title = movie['title'].gsub(/[\"]/,'')
    description = movie['overview'].gsub(/[\"]/,'')
    release_date = movie['release_date']
    length = movie['runtime']
    poster_path = movie['poster_path'] ? 'https://www.themoviedb.org/t/p/w600_and_h900_bestv2' + movie['poster_path'] : ''
    rating = movie['vote_average'] ? movie['vote_average'].round(2) : 5

    # p title, description, release_date, length, poster_path, 'Stills_path', rating

    if description != ''
      insert_string = %{INSERT INTO [Movies] (Title, Description, Release_date, Length, Poster_path, Stills_path, Rating) 
                        VALUES ("#{title}", "#{description}", "#{release_date}", #{length}, "#{poster_path}", "Stills_path", #{rating})}
      client.execute(insert_string).do
      records_movies = records_movies + 1

      p records_movies

      puts 'INSERT MOVIES_GENRES'
      # p movie['genres']
      movie['genres'].each do |movie_genre|
        genres.each do |genre|
          if genre['Name'] == movie_genre['name']

            insert_string = "SELECT Id FROM Movies WHERE Title = \"#{title}\""
            result = client.execute(insert_string)
            movie_id = -1
            result.each do |res|
              movie_id = res['Id']
            end

            insert_string = "INSERT INTO [Movies_Genres] (MovieId, GenreId) VALUES (#{movie_id}, #{genre['Id']})"
            result.cancel
            client.execute(insert_string).do

            records_movies_genres = records_movies_genres + 1
          end
        end
      end
    end
  end
end

puts "#{records_movies} Inserted into Movies"
puts "#{records_movies_genres} Inserted into Movies_Genres"
