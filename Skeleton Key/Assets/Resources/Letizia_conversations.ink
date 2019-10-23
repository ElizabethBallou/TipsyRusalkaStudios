VAR seen_Letizia = false
VAR fire_lit = false

=== Letizia_first_conversation_knot ===
~ character_name = "Old Librarian"
~ seen_Letizia = true
    Who's there? Come closer. I can't see as well as I used to.
    *(Ende_thinks_corpse) [I thought you were dead!]
        -> Ende_is_surprised
    * (Ende_plays_it_cool) [This shit just gets weirder and weirder...]
        -> Ende_is_surprised
        
        = Ende_is_surprised
        {Ende_thinks_corpse: Ah, no. I'm alive, despite my best efforts.}
        Let me touch your face...do I...recognize you? No. You don't feel familiar. What is your name, child?
    
        *[My name is Ende.]
            -> name_Ende
        *[It doesn't matter.]
            -> name_doesnt_matter
        *[I'll tell you my name if you tell me yours first.]
            -> give_your_name
            
    = name_Ende
        Ende. Ende. I knew an Ende, once. You are not her. Your face tells me you are younger. Sharper, too, like a knife.
            -> meaning_of_Letizia
            
    = name_doesnt_matter
        Of course it matters. Living in the Library means losing everything as time goes on. Our names, our faces, the ones we love. I am losing my sight, and that can't be stopped, but I am not going to let go of my name, child. Not until I have to.
            -> meaning_of_Letizia
            
    = give_your_name
    ~ character_name = "Letizia"
        I suppose that's only polite. I am Letizia de Toledo.
        *[Nice to meet you. I'm Ende.]
            -> name_Ende
        *[Call me Ende.]
            -> name_Ende
            
    = meaning_of_Letizia
        ~ character_name = "Letizia"
        {name_doesnt_matter: My name is Letizia de Toledo.} {name_Ende: In any case, I am Letizia de Toledo.} "Letizia" means gladness, did you know that? It was an Italian name first, and then it journeyed across the Mediterranean to Spain, which is how I came by it. And "de Toledo" denotes my birthplace. Toledo. Have you ever heard of Toledo?
            * [Of course. I've been to Toledo.]
                -> Ende_been_to_Toledo
            * [I don't have time for this. Where are we?]
                -> where_are_we
        = Ende_been_to_Toledo
            By the spines! You've been there? Are you Spanish, then? What year were you born?
                * [Spanish? I'm Aragonese. And I don't know the year I was born.]
                    -> Ende_nationality
                * [I hate wasting time. Just tell me where we are.]
                    -> where_are_we

            = Ende_nationality
                Aragonese? In the time I hail from, the kingdom of Aragon stopped existing 150 years ago, when King Ferdinand married Queen Isabella.
                *[When who married who?]<>
                -   I suppose they haven't been born yet, in your world. Ah well. At least that gives me an idea of where you come from.
                    In any case...I ought to tell you where we are.
                    ->where_are_we

    = where_are_we
        {Ende_nationality: This is} The Library of Forking Paths, which is the only place that ever meant anything. If you can find the meaning, that is. I never have.
            -> Ende_asks_questions
    = Ende_asks_questions
            * [What do you mean, "the only place that ever meant anything?"]
                -> only_place_with_meaning
            * [Why do none of the books make sense?]
                -> books_dont_make_sense
            * [How long have you been here?]
                -> how_long_letizia_in_library
            * -> Letizia_gives_quest
        = only_place_with_meaning
            Why, I mean that these books contain all the answers.
            *[Answers to what?]<>
            -   To everything. Every question anyone has ever had. You must not have been here long, or you would know that.
                -> Ende_asks_questions
        = books_dont_make_sense
            The books of the Library contain every possible combination of words. Which means that most of these combinations hold no meaning. Or rather, we simply do not have the key to understand their meaning.
            ->Ende_asks_questions
        = how_long_letizia_in_library
            For three chapters, I believe. It's hard to say for certain.
            *[Chapters?] <>
            -   Oh, my. You really haven't been here long, have you?
                * * [I've been here for...uh...five minutes, give or take?]
                * * (Ende_been_here_too_long) [Too long already.]
                - - {Ende_been_here_too_long: That's what they all say, you know, when they first arrive. Nobody understands what 'too long' means until they've been inside for a while. }
                Time doesn't move forward in a predictable way inside the Library, so we use Chapters to keep track of the Library's history. Each Chapter contains a beginning, a middle, and an end. Just as they do in a book.
                    * * * [What Chapter are we in now?] <>
                    - - - The Chapter of the Locked Doors. It has gone on for a long, long time.
                        ->Ende_asks_questions
    = Letizia_gives_quest
        I am so cold, Ende. I have been lying here for so long. Will you do something for me? Light me a fire in that grate over there. In the center of the room.
        * (Ende_is_greedy) What will I get if I help you? <>
        * (Ende_doesnt_understand) But I still don't understand any of this! <>
        -   {Ende_is_greedy: So very transactional! I do have information to offer you, however.} {Ende_doesnt_understand: The Library does not care if you understand, my dear. And, eventually, neither will you.} Light a fire to warm my old skin, and I will tell you where to find the doors.
            * * Doors? So there ARE ways to get out of here?
            -   - Of course. One must simply know where to look.
                -> DONE
            
=== Letizia_default_conversation_knot ===
    This is default text that appears after you have already spoken to Letizia once.
    -> DONE