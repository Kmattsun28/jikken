import random

class Word:
    def __init__(self, term, definition):
        self.term = term
        self.definition = definition

    def __str__(self):
        return f"{self.term}: {self.definition}"

class WordBook:
    def __init__(self):
        self.words = []

    def add_word(self, term, definition):
        word = Word(term, definition)
        self.words.append(word)

    def remove_word(self, term):
        self.words = [word for word in self.words if word.term != term]

    def find_word(self, term):
        for word in self.words:
            if word.term == term:
                return word
        return None

    def display_words(self):
        for word in self.words:
            print(word)

    def test_random_word(self):
        if not self.words:
            print("単語帳が空です。")
            return
        word = random.choice(self.words)
        answer = input(f"{word.term} の意味は何ですか？: ")
        if answer == word.definition:
            print("正解です！")
        else:
            print(f"不正解です。正しい意味は: {word.definition}")

# 単語帳のインスタンスを作成
word_book = WordBook()

# ユーザーから単語と意味を入力
while True:
    term = input("単語を入力してください（終了するには 'exit' と入力）: ")
    if term.lower() == 'exit':
        break
    definition = input("その単語の意味を入力してください: ")
    word_book.add_word(term, definition)

# 単語帳の内容を表示
    word_book.display_words()
    
    # ランダムに単語をテスト
while True:
    test = input("ランダムに単語をテストしますか？（はい/いいえ）: ")
    if test.lower() == 'いいえ':
        break
    word_book.test_random_word()