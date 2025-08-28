import pandas as pd
df = pd.read_csv("players_22.csv")

# Drop rows with missing league_name
df = df.dropna(subset=['league_name'])

#Take 17000
df = df.head(17000)

# Select relevant columns
df = df[[
    'short_name',
    'long_name',
    'player_positions',
    'overall',
    'age',
    'dob',
    'height_cm',
    'weight_kg',
    'club_name',
    'league_name',
    'nationality_name',
    'preferred_foot',
    'weak_foot',
    'skill_moves',
    'pace',
    'shooting',
    'passing',
    'dribbling',
    'defending',
    'physic',
    'player_face_url',
    'club_logo_url',
    'nation_flag_url'
]]

#Rename columns to match properties in the Player class
df = df.rename(columns={
    'short_name': 'ShortName',
    'long_name': 'LongName',
    'player_positions': 'PlayerPositions',
    'overall': 'Overall',
    'age': 'Age',
    'dob': 'BirthDate',
    'height_cm': 'HeightCm',
    'weight_kg': 'WeightKg',
    'club_name': 'ClubName',
    'league_name': 'LeagueName',
    'nationality_name': 'NationalityName',
    'preferred_foot': 'PreferredFoot',
    'weak_foot': 'WeakFoot',
    'skill_moves': 'SkillMoves',
    'pace': 'Pace',
    'shooting': 'Shooting',
    'passing': 'Passing',
    'dribbling': 'Dribbling',
    'defending': 'Defending',
    'physic': 'Physic',
    'player_face_url': 'PlayerFaceUrl',
    'club_logo_url': 'ClubLogoUrl',
    'nation_flag_url': 'NationFlagUrl'
})

# Convert specified columns to integers, filling NaNs with 0 because Goalkeepers stats are NaN
cols_to_convert = ['Pace', 'Shooting', 'Passing', 'Dribbling', 'Defending', 'Physic']
df[cols_to_convert] = df[cols_to_convert].fillna(0).astype(int)

# Save the cleaned DataFrame to a JSON file
df.to_json("players_cleaned.json", orient="records", lines=False, indent=4)