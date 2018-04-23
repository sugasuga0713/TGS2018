# 超簡易型gitコミット&プッシュ自動化Shell
echo "コミット開始"
echo $(git add .)
echo $(git commit -m "change")
echo $(git push origin master)
echo"コミット終了"