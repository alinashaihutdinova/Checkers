INSERT INTO users (login, password_hash) VALUES
('player1', 'hashed_password1'),
('player2', 'hashed_password2');

INSERT INTO games (white_player_id, black_player_id) VALUES
(1, 2);

INSERT INTO moves (game_id, move_number, player_color, from_position, to_position) VALUES
(1, 1, 'white', 'e2', 'e4'),
(1, 2, 'black', 'e7', 'e5');