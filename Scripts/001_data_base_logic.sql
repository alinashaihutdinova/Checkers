CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL
);

CREATE TABLE games (
    id SERIAL PRIMARY KEY,
    white_player_id INTEGER REFERENCES users(id),
    black_player_id INTEGER REFERENCES users(id),
    started_at TIMESTAMP DEFAULT NOW(),
    finished_at TIMESTAMP,
    winner VARCHAR(10)
);

CREATE TABLE moves (
    id SERIAL PRIMARY KEY,
    game_id INTEGER REFERENCES games(id) ON DELETE CASCADE,
    move_number INTEGER NOT NULL,
    player_color VARCHAR(10) NOT NULL,
    from_position VARCHAR(5) NOT NULL,
    to_position VARCHAR(5) NOT NULL,
    created_at TIMESTAMP DEFAULT NOW()
);